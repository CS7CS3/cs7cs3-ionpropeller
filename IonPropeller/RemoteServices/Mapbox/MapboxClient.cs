using System.Diagnostics;
using System.Web;
using IonPropeller.RemoteServices.Mapbox.Resources;
using IonPropeller.Services.Directions;
using IonPropeller.Services.Geocoding;
using Microsoft.AspNetCore.WebUtilities;

namespace IonPropeller.RemoteServices.Mapbox;

public class MapboxClient
{
    private readonly Uri _baseUri = new("https://api.mapbox.com/");

    private readonly HttpClient _client;

    private readonly IReadOnlyDictionary<string, string> _defaultQueryParams;

    public MapboxClient(MapboxConfiguration configuration)
    {
        if (configuration.AccessToken is not { } accessToken || accessToken.Length == 0)
            throw new ArgumentNullException(nameof(configuration.AccessToken));

        _defaultQueryParams = new Dictionary<string, string> {{"access_token", configuration.AccessToken}};
        _client = new HttpClient();
    }

    public async Task<MapboxDirectionResponse> GetDirection(DirectionProfile profile, LatitudeLongitudeLike origin,
        LatitudeLongitudeLike destination)
    {
        var profileString = profile switch
        {
            DirectionProfile.Cycling => "mapbox/cycling",
            DirectionProfile.Driving => "mapbox/driving",
            DirectionProfile.Walking => "mapbox/walking",
            _ => throw new ArgumentOutOfRangeException(nameof(profile), profile, null)
        };
        var path = $"{origin.Longitude},{origin.Latitude};{destination.Longitude},{destination.Latitude}";

        var uri = new Uri(_baseUri, $"directions/v5/{profileString}/{path}");
        return await GetAsync<MapboxDirectionResponse>(uri.ToString(), new Dictionary<string, string>
        {
            {"geometries", "geojson"}
        });
    }

    public async Task<MapboxGeocodingResponse> GetGeocodingForward(string search, MapboxGeocodingRequest request)
    {
        var uri = new Uri(_baseUri, $"geocoding/v5/mapbox.places/{HttpUtility.UrlEncode(search)}.json");
        var query = request.GetQueryParameters();
        return await GetAsync<MapboxGeocodingResponse>(uri.ToString(), query);
    }

    public async Task<MapboxGeocodingResponse> GetGeocodingReverse(double latitude, double longitude, string[] types,
        uint limit)
    {
        if (types.Length > 1 && limit > 1)
            throw new ArgumentException("Cannot specify multiple types and limit more than 1");

        return await GetGeocodingReverse(latitude, longitude,
            new MapboxGeocodingRequest {Limit = limit, Types = types});
    }

    private async Task<MapboxGeocodingResponse> GetGeocodingReverse(double latitude, double longitude,
        MapboxGeocodingRequest request)
    {
        var uri = new Uri(_baseUri, $"geocoding/v5/mapbox.places/{longitude},{latitude}.json");
        var query = request.GetQueryParameters();
        return await GetAsync<MapboxGeocodingResponse>(uri.ToString(), query);
    }

    private async Task<T> GetAsync<T>(string uri, IReadOnlyDictionary<string, string> query)
    {
        var requestUri = QueryHelpers.AddQueryString(uri, query!);

        var response = await _client.GetAsync(QueryHelpers.AddQueryString(requestUri, _defaultQueryParams!));
        try
        {
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>() ?? throw new Exception("Failed to parse response");
        }
        catch
        {
            if (Debugger.IsAttached)
            {
                var _ = await response.Content.ReadAsStringAsync();
                Debugger.Break();
            }

            throw;
        }
    }
}