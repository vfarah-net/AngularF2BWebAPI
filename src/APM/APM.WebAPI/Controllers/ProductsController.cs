using System;
using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Http;
using APM.Domain.Model;
using APM.Domain.Repository;

namespace APM.WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository productRepository;

        public ProductsController()
        {
            this.productRepository = new ProductTextRepository(HostingEnvironment.MapPath(@"~/App_Data/product.json"));
        }
        // GET: api/Products
        public IEnumerable<Product> Get()
        {
            return productRepository.Retrieve();
        }

        // GET: api/Products/5
        public Product Get(int id)
        {
            return productRepository.FindProductId(id);
        }

        // POST: api/Products
        public void Post([FromBody]Product value)
        {
            productRepository.Save(value);
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]Product value)
        {
            if (value.ProductId == id)
            {
                productRepository.Save(value);
            }            
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
            productRepository.Delete(id);
        }
    }
}
