using Microsoft.AspNetCore.Mvc;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceModel.ViewModel;
using Sales_EcommerceService.IService;

namespace Sales_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody]ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var uploadedImagePath = await _productService.UploadProductImage(product.ImageUrl);
                if (uploadedImagePath == "false")
                {
                    ModelState.AddModelError("ImageUrl", "Failed to upload image.");
                    return BadRequest(ModelState);
                }
                var productEntity = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category,
                    Popularity = product.Popularity,
                    ImageUrl = uploadedImagePath
                };
                var result = await _productService.AddProductAsync(productEntity);
                if (result)
                {
                    return RedirectToAction("GetAllProducts");
                }
                ModelState.AddModelError("", "Failed to add product.");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products != null && products.Count > 0)
            {
                return Ok(products);
            }
            return NotFound("No products found.");
        }
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound($"Product with ID {id} not found.");
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProductAsync(product);
                if (result)
                {
                    return Ok("Product updated successfully.");
                }
                ModelState.AddModelError("", "Failed to update product.");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result)
            {
                return Ok("Product deleted successfully.");
            }
            return NotFound($"Product with ID {id} not found.");
        }
    }
}
