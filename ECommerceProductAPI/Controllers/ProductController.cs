using ECommerceProductAPI.Dto;
using ECommerceProductAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProductAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]     
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
           public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
            {
                if (product == null)
                {
                    return BadRequest("Product data is null.");
                }
                var result = await _productRepository.AddProductAsync(product);
                if (!result)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add product.");
                }
                return Ok("Product added successfully.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }
            var result = await _productRepository.UpdateProductAsync(id, product);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update product.");
            }
            return Ok("Product updated successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProductAsync(id);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete product.");
            }
            return Ok("Product deleted successfully.");
        }
    }
}