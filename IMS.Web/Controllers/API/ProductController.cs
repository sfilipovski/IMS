using IMS.Domain.DTO.Command;
using IMS.Domain.DTO.Query;
using IMS.Domain.Models;
using IMS.Repository.Implementation;
using IMS.Repository.Interface;
using IMS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ProductRepository productRepository;

        public ProductController(IProductService productService, ProductRepository productRepository)
        {
            _productService = productService;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            productRepository.Create(product);

            return Ok(product);
        }

        [HttpPut]
        public IActionResult UpdateProduct(int id, Product update)
        {
            _productService.UpdateProduct(id, update);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);

            return NoContent();
        }

        [HttpGet("getAll")]
        public ActionResult<List<Product>> GetAll()
        {
            return Ok(productRepository.GetAll());
        }
    }
}
