using Microsoft.AspNetCore.Http;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceRepository.IRepository;
using Sales_EcommerceService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Sales_EcommerceService.Service
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int Id)
        {
            return await _productRepository.DeleteProductAsync(Id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            return await _productRepository.GetProductByIdAsync(Id);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<string>UploadProductImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "false";
            }
            try
            {

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                // ✅ Ensure the folder exists
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return "false";
            }
        }
    }
}
