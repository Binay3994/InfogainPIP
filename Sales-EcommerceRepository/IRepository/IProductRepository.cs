using Sales_EcommerceModel.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceRepository.IRepository
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int Id);
        Task<Product> GetProductByIdAsync(int Id);
        Task<List<Product>> GetAllProductsAsync();

    }
}
