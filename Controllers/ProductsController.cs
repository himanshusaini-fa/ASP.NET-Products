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
        //private readonly IApiKeyRepo _apiKeyRepo;

        public ProductsController(IProductsRepo repository, IMapper mapper/*, IApiKeyRepo apiKeyRepo*/)
        {
            _repository = repository;
            _mapper = mapper;
            //_apiKeyRepo = apiKeyRepo;
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProduct()
        {
            // if (!_apiKeyRepo.Authenticated("1234567891234567"))
            // {
            //     return StatusCode(StatusCodes.Status401Unauthorized, "The Api Key provided is not valid");
            // }
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
        [HttpGet("delete/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var productFromRepo = _repository.GetProductById(id);
                if (productFromRepo == null) return NotFound();
                _repository.DeleteProduct(productFromRepo);
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

        [HttpPut("{id}")]
        public ActionResult FullUpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var productFromRepo = _repository.GetProductById(id);
            if (productFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(productUpdateDto, productFromRepo);
            _repository.FullUpdateProduct(productFromRepo);
            if (_repository.SaveChanges())
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
