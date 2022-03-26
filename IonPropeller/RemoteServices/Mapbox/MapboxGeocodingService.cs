using IonPropeller.RemoteServices.Mapbox.Resources;
using IonPropeller.Services.Geocoding;

namespace IonPropeller.RemoteServices.Mapbox;

public class MapboxGeocodingService : IGeocodingService
{
    private readonly MapboxClient _client;

    public MapboxGeocodingService(MapboxClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<GeocodingFeature>> QueryForward(string address, double latitude, double longitude)
    {
        var request = new MapboxGeocodingRequest
        {
            Limit = 5,
            Position = new GeocodingFeaturePosition {Latitude = latitude, Longitude = longitude}
        };
        return (await _client.Forward(address, request)).Features.Select(f => (GeocodingFeature) f);
    }

    public async Task<IEnumerable<GeocodingFeature>> QueryReverse(double latitude, double longitude)
    {
        return await QueryReverse(latitude, longitude, MapboxGeocodingRequest.DefaultRequestTypes);
    }

    public async Task<IEnumerable<GeocodingFeature>> QueryReverse(double latitude, double longitude, string[] types)
    {
        return (await _client.Reverse(latitude, longitude, types)).Features.Select(f => (GeocodingFeature) f);
    }
}