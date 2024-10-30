using Engage.Keyloop.Api.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;


namespace Engage.Keyloop.Api.Handlers;

public class OAuthHandler : DelegatingHandler
{
    private readonly ILogger<OAuthHandler> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly KeyloopConfiguration _config;

    public OAuthHandler(ILogger<OAuthHandler> logger, IHttpClientFactory clientFactory, KeyloopConfiguration config)
    {
        _logger = logger;
        _clientFactory = clientFactory;
        _config = config;

        _httpClient = _clientFactory.CreateClient(nameof(OAuthHandler));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Retrieve the access token
        string accessToken = await RequestAccessTokenAsync();

        // Add the token to the Authorization header
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Continue with the request pipeline
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> RequestAccessTokenAsync()
    {
        var requestBody = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", _config.ClientId),
                    new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
                    new KeyValuePair<string, string>("scope", _config.Scopes)
            });

        HttpResponseMessage response = await _httpClient.PostAsync("/oauth/client_credential/accesstoken", requestBody);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var tokenResponse = System.Text.Json.JsonDocument.Parse(responseBody);
            return tokenResponse.RootElement.GetProperty("access_token").GetString();
        }
        else
        {
            throw new HttpRequestException($"Token request failed with status code {response.StatusCode}");
        }
    }
}
