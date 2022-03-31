using GeoJSON.Text.Feature;
using IonPropeller.Services.Geocoding;

namespace IonPropeller.Services.Directions;

public interface IDirectionService
{
    Task<FeatureCollection> GetDirections(LatitudeLongitudeLike origin, LatitudeLongitudeLike destination,
        DirectionProfile profile);
}