﻿using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context) 
        {
            _context = context;
        }
        public async Task CreateProduct(Product product)
        {
           await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult delete = await _context.Products.DeleteOneAsync(filter);
            return delete.IsAcknowledged && delete.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
           return  await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var update = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return update.IsAcknowledged && update.ModifiedCount > 0;
        }
    }
}
