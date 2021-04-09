using FreshFishWebsite.Extensions;
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FreshFishDbContext _context;

        public OrderRepository(FreshFishDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.GetOrdersWithUsers();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.GetOrderById(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Order> GetOrderWithUserAndProductsWithStorages(int orderId)
        {
            return await _context.Orders.GetOrderWithUserAndProductsWithStorages(orderId);
        }

        public async Task SendOrderToStorages(int orderId)
        {
            var order = await GetOrderWithUserAndProductsWithStorages(orderId);
            var storages = GetStorages(order);
            await SetOrderItemsIntoStorages(storages, order);
            order.IsOrderAssigned = true;
            await UpdateAsync(order);
        }

        private static IEnumerable<Storage> GetStorages(Order order)
        {
            return order.Products.Select(x => x.Product.Storage).Distinct();
        }

        private async Task SetOrderItemsIntoStorages(IEnumerable<Storage> storages, Order order)
        {
            foreach (var s in storages)
            {
                var orderItems = new List<OrderItems>
                {
                    new OrderItems
                    {
                        Order = order,
                        Storage = s
                    }
                };

                s.OrderItems.Add(orderItems.FirstOrDefault(x => x.Storage.Id == s.Id));
                await _context.OrderItems.AddAsync(orderItems.FirstOrDefault(x => x.Storage.Id == s.Id));
                _context.Storages.Update(s);
            }
        }
    }
}
