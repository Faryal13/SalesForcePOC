using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class SalesforceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _username;
    private readonly string _password;
    private readonly string _tokenUrl;

    public SalesforceClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _clientId = config["Salesforce:ClientId"];
        _clientSecret = config["Salesforce:ClientSecret"];
        _username = config["Salesforce:Username"];
        _password = config["Salesforce:Password"];
        _tokenUrl = config["Salesforce:TokenUrl"];
    }

    public async Task<string> GetSalesforceDataAsync()
    {
        var formBody = $"grant_type=password" +
                       $"&client_id={_clientId}" +
                       $"&client_secret={_clientSecret}" +
                       $"&username={_username}" +
                       $"&password={_password}";

        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, _tokenUrl)
        {
            Content = new StringContent(formBody, Encoding.UTF8, "application/x-www-form-urlencoded")
        };

        var tokenResponse = await _httpClient.SendAsync(tokenRequest);
        tokenResponse.EnsureSuccessStatusCode();

        var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
        var tokenData = JsonSerializer.Deserialize<JsonElement>(tokenContent);
        var accessToken = tokenData.GetProperty("access_token").GetString();
        var instanceUrl = tokenData.GetProperty("instance_url").GetString();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        string queryUrl = $"{instanceUrl}/services/data/v59.0/query?q=SELECT+Name+FROM+Account";

        var queryResponse = await _httpClient.GetAsync(queryUrl);
        queryResponse.EnsureSuccessStatusCode();

        return await queryResponse.Content.ReadAsStringAsync();
    }
}
