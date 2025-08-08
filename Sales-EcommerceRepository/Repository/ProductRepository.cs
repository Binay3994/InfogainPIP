using Microsoft.EntityFrameworkCore;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceRepository.Context;
using Sales_EcommerceRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceRepository.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly Sales_EcommerceDbContext _EcommerceDbContext;
        public ProductRepository(Sales_EcommerceDbContext EcommerceDbContext)
        {
            _EcommerceDbContext = EcommerceDbContext;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
             await _EcommerceDbContext.Product.AddAsync(product);
            return await _EcommerceDbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<bool> DeleteProductAsync(int Id)
        {  
            return await _EcommerceDbContext.Product
                .Where(p => p.ProductId == Id)
                .ExecuteDeleteAsync()
                .ContinueWith(t => t.Result > 0);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _EcommerceDbContext.Product
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
           return await _EcommerceDbContext.Product
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == Id);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _EcommerceDbContext.Product
                .Where(p => p.ProductId == product.ProductId)
                .ExecuteUpdateAsync(upd => upd
                    .SetProperty(p => p.Name, product.Name)
                    .SetProperty(p => p.Description, product.Description)
                    .SetProperty(p => p.Price, product.Price)
                    .SetProperty(p => p.Category, product.Category)
                    .SetProperty(p => p.Popularity, product.Popularity)
                    .SetProperty(p => p.ImageUrl, product.ImageUrl)
                    .SetProperty(p => p.UpdatedAt, DateTime.UtcNow))
                .ContinueWith(t => t.Result > 0);
        }
    }
}
