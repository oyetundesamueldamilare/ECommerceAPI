using ECommerceProductAPI.Dto;

namespace ECommerceProductAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(ProductDto product);

        Task<List<ProductDto>> GetAllProductsAsync();

        Task<ProductDto?> GetProductByIdAsync(int id);

        Task<bool> UpdateProductAsync(int id, ProductDto product);

        Task<bool> DeleteProductAsync(int id);
    }
}
