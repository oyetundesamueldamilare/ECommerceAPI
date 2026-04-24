using ECommerceProductAPI.Data;
using Microsoft.EntityFrameworkCore;
using ECommerceProductAPI.Dto;
using ECommerceProductAPI.Interfaces;
using ECommerceProductAPI.Models;

namespace ECommerceProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProductAsync(ProductDto product)
        {
            if (product == null) return false;

            var prod = new Product
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductCode = product.ProductCode,
                Price = product.Price,
                Quantity = product.Quantity
            };

            _context.Products.Add(prod);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductCode = p.ProductCode,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToListAsync();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductCode = product.ProductCode,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.ProductName = productDto.ProductName;
            product.ProductCode = productDto.ProductCode;
            product.Price = productDto.Price;
            product.Quantity = productDto.Quantity;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}