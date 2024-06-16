using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);    
        }
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(string id)
        {
            var products = await _productRepository.GetProduct(id);
            if(products is null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            if (category is null)
            {
                return BadRequest("Invalid category");
            }
            var products = await _productRepository.GetProductByCategory(category);
            
            return Ok(products);
        }
        [HttpPut]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Product>>> UpdateProduct([FromBody] Product product)
        {
            if (product is null)
            {
                return BadRequest("Invalid product");
            }
            var products = await _productRepository.UpdateProduct(product);

            return Ok(products);
        }
        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteProductById(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }
    }
}
