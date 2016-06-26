using System;
using System.Collections.Generic;
using APM.Domain.Model;

namespace APM.Domain.Repository
{
    public interface IProductRepository
    {
        Product Create();
        IEnumerable<Product> Retrieve();
        Product FindProductId(int productId);
        IEnumerable<Product> Search(string search, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase);
        Product Save(Product product);
        void Delete(int id);
    }
}