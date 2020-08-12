using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Dtos;
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
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProduct()
        {
            var products = _repository.GetAllProducts();
            try
            {
                return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Fatal Error");
            }
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public ActionResult<ProductReadDto> GetProductById(int id)
        {
            var product = _repository.GetProductById(id);

            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"{id} not Found");
            }

            return Ok(_mapper.Map<ProductReadDto>(product));
        }

        // Post: api/products/
        [HttpPost]
        public ActionResult<Product> AddProduct(ProductAddDto productAddDto)
        {
            var product = _mapper.Map<Product>(productAddDto);
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
