using Microsoft.AspNetCore.Http;
using Sales_EcommerceModel.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceService.IService
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int Id);
        Task<Product> GetProductByIdAsync(int Id);
        Task<List<Product>> GetAllProductsAsync();
        Task<string> UploadProductImage(IFormFile file);
    }
}
