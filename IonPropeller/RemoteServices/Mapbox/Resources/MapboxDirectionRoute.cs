using System.Text.Json.Serialization;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;

namespace IonPropeller.RemoteServices.Mapbox.Resources;

public class MapboxDirectionRoute
{
    [JsonPropertyName("geometry")] public LineString Geometry { get; set; }

    public Feature ToFeature()
    {
        return new Feature(Geometry);
    }
}