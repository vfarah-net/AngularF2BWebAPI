using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using APM.Domain.Model;
using APM.Domain.Repository;

namespace APM.WebAPI.Controllers
{
    [EnableCors("http://localhost:51735","*","*")]
    public class ProductsController : ApiController
    {
        private readonly IProductRepository productRepository;

        public ProductsController()
        {
            this.productRepository = new ProductTextRepository(HostingEnvironment.MapPath(@"~/App_Data/product.json"));
        }

        // GET: api/Products and ODATA stuff now
        [EnableQuery(PageSize = 50)]
        public IQueryable<Product> Get()
        {
            return productRepository.Retrieve().AsQueryable();
        }

        ///<remarks>NOTE: This method becomes redundent with the use of ODATA</remarks>
        //GET api/Products? search = { search }
        public IEnumerable<Product> Get(string search)
        {
            return productRepository.Search(search);
        }

        // GET: api/Products/5
        public Product Get(int id)
        {
            return id>0?productRepository.FindProductId(id):new Product();
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
