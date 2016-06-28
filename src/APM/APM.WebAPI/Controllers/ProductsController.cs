using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
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
        //To facilitate the help documentation that has stopped workimg>    
        [ResponseType(typeof(Product))] 
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(productRepository.Retrieve().AsQueryable());
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }            
        }

        ///<remarks>NOTE: This method becomes redundent with the use of ODATA</remarks>
        //GET api/Products? search = { search }
        //public IEnumerable<Product> Get(string search)
        //{
        //    return productRepository.Search(search);
        //}

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                //Test scenario to see the messages that get passed back to the client
                //throw new Exception("This message is passed back to the client");
                var product = id > 0 ? productRepository.FindProductId(id) : new Product();
                if (product != null)
                {
                    return Ok(product);
                }
                return NotFound();
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]Product value)
        {
            try
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
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]Product value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Product can not be null");
                }

                if (value.ProductId != id)
                {
                    return BadRequest("The id did not match");
                }

                productRepository.Save(value);
                return Ok();
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        // DELETE: api/Products/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                productRepository.Delete(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}
