
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
    public class ProductRepository : IRepository<Product>
    {
        private readonly FreshFishDbContext _;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductRepository(FreshFishDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _ = context;
        }
        public IEnumerable<Product> GetAll()
        => _.Products;

        public Product GetById(int? id)
        => _.Products.FirstOrDefault(p => p.Id == id);

        public async Task AddAsync(Product newItem)
        {
            await SaveImageAsync(newItem);
            await _.Products.AddAsync(newItem);
            await _.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product item)
        {
            if (item.ImageFile != null)
            {
                DeleteImage(item);
                await SaveImageAsync(item);
            }
            _.Products.Update(item);
            await _.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = GetById(id);
            if(product != null)
            {
                DeleteImage(product);
                _.Products.Remove(product);
                await _.SaveChangesAsync();
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
