using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetMyProducts());
            }
        }
        private static IEnumerable<Product> GetMyProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "4357953845846387f76e8r6347428",
                    Name = "Texto teste 1",
                    Description = "Description",
                    Image = "product-1.png",
                    Price = 9500,
                    Category = "Category 1"
                },
                new Product()
                {
                    Id = "4357953845834543587f76e8r6347428",
                    Name = "Texto teste 2",
                    Description = "Description",
                    Image = "product-2.png",
                    Price = 488,
                    Category = "Category 2"
                },
                new Product()
                {
                    Id = "4357953rtt354g46387f76e8r6347428",
                    Name = "Texto teste 3",
                    Description = "Description",
                    Image = "product-3.png",
                    Price = 67,
                    Category = "Category 3"
                },
            };
        }
    }
}
