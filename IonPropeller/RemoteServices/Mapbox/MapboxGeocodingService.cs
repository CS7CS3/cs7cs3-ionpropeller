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
            Position = new LatitudeLongitudeLike {Latitude = latitude, Longitude = longitude}
        };
        return (await _client.GetGeocodingForward(address, request)).Features.Select(f => (GeocodingFeature) f);
    }

    public async Task<IEnumerable<GeocodingFeature>> QueryReverse(double latitude, double longitude)
    {
        return await QueryReverse(latitude, longitude, MapboxGeocodingRequest.DefaultRequestTypes, 1);
    }

    public async Task<IEnumerable<GeocodingFeature>> QueryReverse(double latitude, double longitude, string[] types,
        uint limit)
    {
        return (await _client.GetGeocodingReverse(latitude, longitude, types, limit)).Features.Select(f =>
            (GeocodingFeature) f);
    }
}