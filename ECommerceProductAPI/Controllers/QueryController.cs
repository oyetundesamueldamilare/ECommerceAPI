using ECommerceProductAPI.Dto;
using ECommerceProductAPI.Repository;
using ECommerceProductAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IProductQueryRepository _repository;
        private readonly ILogger<QueryController> _logger;
        public QueryController(IProductQueryRepository repository, ILogger<QueryController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetAll([FromQuery] ProductQueryParams queryParams)
        {
            _logger.LogInformation(
                "GetAll products — page: {Page}, size: {Size}, sortBy: {SortBy}, sortDesc: {SortDesc}, name: {ProductName}, code: {ProductCode}, productId: {ProductId}",
                queryParams.PageNumber, queryParams.PageSize, queryParams.SortBy, queryParams.SortDesc,
                queryParams.ProductName, queryParams.ProductCode, queryParams.Id);

            var result = await _repository.GetAllAsync(queryParams);
            _logger.LogInformation("Returned {Count} of {Total} products.", result.Items.Count, result.TotalCount);
            return Ok(result);
        }
    }
}
