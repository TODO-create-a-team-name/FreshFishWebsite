using FreshFishWebsite.Extensions;
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FreshFishDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductRepository(FreshFishDbContext context,
            IWebHostEnvironment hostEnvironment = null)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        => _context.Products;

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.GetProductByIdAsync(id);
        }

        public IEnumerable<Product> GetProductsByStorageId(int storageId)
        {
            return _context.Products.GetProductsByStorageId(storageId);
        }

        public async Task AddAsync(Product newItem)
        {
            await SaveImageAsync(newItem);
            await _context.Products.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product item)
        {
            if (item.ImageFile != null)
            {
                DeleteImage(item);
                await SaveImageAsync(item);
            }
            _context.Products.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                DeleteImage(product);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void DeleteImage(Product product)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath,
                "images/productsImages",
                product.Image);

            if (File.Exists(imagePath))
                File.Delete(imagePath);
        }
        private async Task<Product> SaveImageAsync(Product product)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            //Save image to wwwroot/image/news
            if (product.ImageFile == null)
            {
                product.Image = "/default.png";
            }
            else
            {
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                product.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/images/productsImages", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }
            }
            return product;
        }
    }
}
