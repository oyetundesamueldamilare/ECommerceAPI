using ECommerceProductAPI.Dto;
using ECommerceProductAPI.Services;
using System.Linq.Dynamic.Core;

namespace ECommerceProductAPI.Repository
{
    public interface IProductQueryRepository
    {
        Task<PagedResult<ProductDto>> GetAllAsync(ProductQueryParams q);
    }
}