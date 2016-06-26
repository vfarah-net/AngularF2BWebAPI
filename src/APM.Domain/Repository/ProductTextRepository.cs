using System;
using System.Collections.Generic;
using System.Linq;
using APM.Domain.Model;
using Newtonsoft.Json;
using APM.Domain.Extensions;

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

        public IEnumerable<Product> Retrieve()
        {
            var json = System.IO.File.ReadAllText(filePath);

            var products = JsonConvert.DeserializeObject<List<Product>>(json);

            return products;
        }

        public Product FindProductId(int productId)
        {
            return Retrieve().SingleOrDefault(each => each.ProductId == productId);
        }

        public IEnumerable<Product> Search(string search, StringComparison stringComparison)
        {
            if (search != null)
            {
                return Retrieve().Where(each => each.ProductCode.Contains(search, stringComparison) || 
                each.ProductName.Contains(search, stringComparison));
            }
            return Retrieve();
        }

        public Product Save(Product product)
        {
            var products = this.Retrieve().ToList();
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
