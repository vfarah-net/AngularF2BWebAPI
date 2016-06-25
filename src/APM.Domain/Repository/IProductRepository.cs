using System;
using System.Collections.Generic;
using APM.Domain.Model;

namespace APM.Domain.Repository
{
    public interface IProductRepository
    {
        Product Create();
        IList<Product> Retrieve();
        Product FindProductId(int productId);
        IList<Product> Search(string search, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);
        Product Save(Product product);
        void Delete(int id);
    }
}