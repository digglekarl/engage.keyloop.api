using Engage.Keyloop.Api.Interface;
using Engage.Keyloop.Api.Models.Request;
using Engage.Keyloop.Api.Models.Response;
using System.Text.Json;

namespace Engage.Keyloop.Api.Services;

public class KeyloopService : IKeyloopService
{
    private readonly ILogger<KeyloopService> _logger;
    private readonly HttpClient _httpClient;

    public KeyloopService(ILogger<KeyloopService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger; 
        _httpClient = httpClientFactory.CreateClient(nameof(KeyloopService));
    }

    public async Task<BrandResponse> GetBrandsAsync()
    {
        var response = await _httpClient.GetAsync("/sample/sample/v1/parts/brands");
        if (response.IsSuccessStatusCode)
        {
            var brandResponse = await response.Content.ReadFromJsonAsync<BrandResponse>();
            return brandResponse;
        }
        else
        {
            _logger.LogError("Error getting brands");
            return null;
        }
    }

    public async Task<CustomerResponse> GetCustomersAsync()
    {
        var response = await _httpClient.GetAsync("/sample/sample/v3/customers?freeTextSearch=C");
        if (response.IsSuccessStatusCode)
        {
            var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
            return customerResponse;
        }
        else
        {
            _logger.LogError("Error getting customers");
            return null;
        }
    }

    public async Task<PartDetailsResponse> GetPartDetailsAsync(string partId)
    {
        var response = await _httpClient.GetAsync($"/sample/sample/v1/parts");
        if (response.IsSuccessStatusCode)
        {
            var partDetails = await response.Content.ReadFromJsonAsync<PartDetailsResponse>();
            return partDetails;
        }
        else
        {
            _logger.LogError($"Error getting part details for {partId}");
            return null;
        }
    }

    public async Task<PartsResponse> SearchPartAsync(SearchPartRequest request)
    {
        var response = await _httpClient.PostAsync($"/sample/sample/v1/parts?brandCode={request.BrandCode}&partCode={request.PartCode}", new StringContent(JsonSerializer.Serialize(request)));
        if (response.IsSuccessStatusCode)
        {
            var part = await response.Content.ReadFromJsonAsync<PartsResponse>();
            return part;
        }
        else
        {
            _logger.LogError($"Error getting part details for {request.BrandCode} {request.PartCode}");
            return null;
        }
    }
}
