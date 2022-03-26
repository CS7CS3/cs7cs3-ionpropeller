using System.Web;
using IonPropeller.RemoteServices.Mapbox.Resources;
using Microsoft.AspNetCore.WebUtilities;

namespace IonPropeller.RemoteServices.Mapbox;

public class MapboxClient
{
    private readonly Uri _baseUri = new("https://api.mapbox.com/");

    private readonly HttpClient _client;

    private readonly IReadOnlyDictionary<string, string> _defaultQueryParams;

    public MapboxClient(MapboxConfiguration configuration)
    {
        _defaultQueryParams = new Dictionary<string, string> {{"access_token", configuration.AccessToken}};
        _client = new HttpClient();
    }

    public async Task<MapboxGeocodingResponse> Forward(string search, MapboxGeocodingRequest request)
    {
        var uri = new Uri(_baseUri, $"geocoding/v5/mapbox.places/{HttpUtility.UrlEncode(search)}.json");
        var query = request.GetQueryParameters();
        return await GetAsync<MapboxGeocodingResponse>(uri.ToString(), query!);
    }

    public async Task<MapboxGeocodingResponse> Reverse(double latitude, double longitude, string[] types)
    {
        return await Reverse(latitude, longitude, new MapboxGeocodingRequest {Types = types});
    }

    private async Task<MapboxGeocodingResponse> Reverse(double latitude, double longitude,
        MapboxGeocodingRequest request)
    {
        var uri = new Uri(_baseUri, $"geocoding/v5/mapbox.places/{longitude},{latitude}.json");
        var query = request.GetQueryParameters();
        return await GetAsync<MapboxGeocodingResponse>(uri.ToString(), query!);
    }

    private async Task<T> GetAsync<T>(string uri, IReadOnlyDictionary<string, string?> query)
    {
        var requestUri = QueryHelpers.AddQueryString(uri, query);

        var response = await _client.GetAsync(QueryHelpers.AddQueryString(requestUri, _defaultQueryParams!));
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>() ?? throw new Exception("Failed to parse response");
    }
}