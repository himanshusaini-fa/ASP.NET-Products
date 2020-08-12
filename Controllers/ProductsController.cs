using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;

namespace Products.Controllers
{
    [Route("api/products")]
    [ApiController]
    //For user authorization to use api
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepo _repository;

        public ProductsController(IProductsRepo repository)
        {
            _repository = repository;
        }

        // GET: api/products
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

        // GET: api/products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _repository.GetProductById(id);

            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"{id} not Found");
            }

            return Ok(product);
        }

        // Post: api/products/
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            try
            {
                _repository.AddProduct(product);
                if (_repository.SaveChanges())
                {
                    return CreatedAtAction(nameof(GetProductById), new { Id = product.Id }, product);
                }

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var productInDB = _repository.GetProductById(id);
                if (productInDB == null) return NotFound();
                _repository.DeleteProduct(productInDB);
                if (_repository.SaveChanges())
                {
                    return NoContent();
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the product");
        }

    }
}
