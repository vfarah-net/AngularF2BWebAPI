using System;
using System.Collections.Generic;
using System.Linq;
using APM.Domain.Model;
using Newtonsoft.Json;

namespace APM.Domain.Repository
{
    public class ProductTextRepository : IProductRepository
    {
        private string filePath;

        public ProductTextRepository(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            this.filePath = filePath;
        }

        public Product Create()
        {
            Product product = new Product
            {
                ReleaseDate = DateTime.Now
            };
            return product;
        }

        public IList<Product> Retrieve()
        {
            var json = System.IO.File.ReadAllText(filePath);

            var products = JsonConvert.DeserializeObject<List<Product>>(json);

            return products;
        }

        public Product FindProductId(int productId)
        {
            return Retrieve().SingleOrDefault(each => each.ProductId == productId);
        }

        public Product Save(Product product)
        {
            var products = this.Retrieve();
            // Assign a new Id
            var maxId = products.Max(p => p.ProductId);
            product.ProductId = maxId + 1;
            products.Add(product);
            WriteData(products);
            return product;
        }

        public void Delete(int id)
        {
            var products = Retrieve().ToList();
            if (products.RemoveAll(each => each.ProductId == id) > 0)
            {
                WriteData(products);
            }
            
        }

        private bool WriteData(IList<Product> products)
        {
            // Write out the Json
            var json = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
            return true;
        }
    }
}
