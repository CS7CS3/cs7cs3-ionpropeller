using System.Text.Json.Serialization;
using GeoJSON.Text.Feature;

namespace IonPropeller.RemoteServices.Mapbox.Resources;

public class MapboxDirectionResponse
{
    [JsonPropertyName("routes")] public MapboxDirectionRoute[] Routes { get; set; }

    public FeatureCollection ToFeatures()
    {
        return new FeatureCollection(Routes.Select(r => r.ToFeature()).ToList());
    }
}