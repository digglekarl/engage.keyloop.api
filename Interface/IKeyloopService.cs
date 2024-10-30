using Engage.Keyloop.Api.Models.Request;
using Engage.Keyloop.Api.Models.Response;

namespace Engage.Keyloop.Api.Interface;

public interface IKeyloopService
{
    Task<BrandResponse> GetBrandsAsync();
    Task<PartsResponse> SearchPartAsync(SearchPartRequest request);
    Task<PartDetailsResponse> GetPartDetailsAsync(string partId);

    Task<CustomerResponse> GetCustomersAsync();
}
