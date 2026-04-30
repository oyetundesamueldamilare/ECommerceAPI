using ECommerceProductAPI.Data;
using ECommerceProductAPI.Dto;
using ECommerceProductAPI.Services;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProductAPI.Repository
{
    public class ProductQueryRepository         : IProductQueryRepository
    {
        private readonly AppDbContext _context;

        public ProductQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ProductDto>> GetAllAsync(ProductQueryParams q)
        {
            var query = _context.Products.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(q.ProductName))
                query = query.Where(e => e.ProductName.Contains(q.ProductName));
            if (!string.IsNullOrWhiteSpace(q.ProductCode))
                query = query.Where(e => e.ProductCode.Contains(q.ProductCode));
            if (q.Id.HasValue)
                query = query.Where(e => e.Id == q.Id.Value);

            var totalCount = await query.CountAsync();

            // Sorting
            query = q.SortBy?.ToLower() switch
            {
                "name" => q.SortDesc ? query.OrderByDescending(e => e.ProductName) : query.OrderBy(e => e.ProductName),
                "code" => q.SortDesc ? query.OrderByDescending(e => e.ProductCode) : query.OrderBy(e => e.ProductCode),
                "price" => q.SortDesc ? query.OrderByDescending(e => e.Price) : query.OrderBy(e => e.Price),
                "quantity" => q.SortDesc ? query.OrderByDescending(e => e.Quantity) : query.OrderBy(e => e.Quantity),
                _ => query.OrderBy(e => e.Id)
            };

            // Paging
            var items = await query
                .Skip((q.PageNumber - 1) * q.PageSize)
                .Take(q.PageSize)
                .Select(e => new ProductDto
                {
                    Id = e.Id,
                    ProductName = e.ProductName,
                    ProductCode = e.ProductCode,
                    Price = e.Price,
                    Quantity = e.Quantity,
                })
                .ToListAsync();

            return new PagedResult<ProductDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = q.PageNumber,
                PageSize = q.PageSize
            };
        }
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
