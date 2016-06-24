using System.Collections.Generic;
using APM.Domain.Model;

namespace APM.Domain.Repository
{
    public interface IProductRepository
    {
        Product Create();
        IList<Product> Retrieve();
        Product FindProductId(int productId);
        Product Save(Product product);
        void Delete(int id);
    }
}