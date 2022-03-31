using GeoJSON.Text.Feature;
using IonPropeller.Services.Directions;
using IonPropeller.Services.Geocoding;

namespace IonPropeller.RemoteServices.Mapbox;

public class MapboxDirectionService : IDirectionService
{
    private readonly MapboxClient _client;

    public MapboxDirectionService(MapboxClient client)
    {
        _client = client;
    }

    public async Task<FeatureCollection> GetDirections(LatitudeLongitudeLike origin, LatitudeLongitudeLike destination,
        DirectionProfile profile)
    {
        return (await _client.GetDirection(profile, origin, destination)).ToFeatures();
    }
}