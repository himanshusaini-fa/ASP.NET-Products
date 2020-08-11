using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;

namespace Products.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepo _repository;

        public ProductsController(IProductsRepo repository)
        {
            _repository = repository;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProduct()
        {
            var products = _repository.GetAllProducts();
            try
            {
                return Ok(products);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Fatal Error");
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _repository.GetProductById(id);

            if (product == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Product Not Found");
            }

            return Ok(product);
        }
    }
}
