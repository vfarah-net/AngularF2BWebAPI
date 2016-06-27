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
    [EnableCors("http://localhost:51735", "*", "*")]
    public class ProductsController : ApiController
    {
        private readonly IProductRepository productRepository;

        public ProductsController()
        {
            this.productRepository = new ProductTextRepository(HostingEnvironment.MapPath(@"~/App_Data/product.json"));
        }

        // GET: api/Products and ODATA stuff now
        [EnableQuery(PageSize = 50)]
        public IHttpActionResult Get()
        {
            return Ok(productRepository.Retrieve().AsQueryable());
        }

        ///<remarks>NOTE: This method becomes redundent with the use of ODATA</remarks>
        //GET api/Products? search = { search }
        //public IEnumerable<Product> Get(string search)
        //{
        //    return productRepository.Search(search);
        //}

        // GET: api/Products/5
        public IHttpActionResult Get(int id)
        {
            var product = id > 0 ? productRepository.FindProductId(id) : new Product();
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]Product value)
        {
            if (value == null)
            {
                return BadRequest("Product can not be null");
            }
            var newProduct = productRepository.Save(value);
            if (newProduct == null)
            {
                return Conflict();
            }
            return Created(Request.RequestUri + newProduct.ProductId.ToString(), newProduct);
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]Product value)
        {
            if (value == null)
            {
                return BadRequest("Product can not be null");
            }

            if (value.ProductId == id)
            {
                productRepository.Save(value);
                return Ok();
            }
            return BadRequest("The id did not match");
        }

        // DELETE: api/Products/5
        public IHttpActionResult Delete(int id)
        {
            productRepository.Delete(id);
            return Ok();
        }
    }
}
