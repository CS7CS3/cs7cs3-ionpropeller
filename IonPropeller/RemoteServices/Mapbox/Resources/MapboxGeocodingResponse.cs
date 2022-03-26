using System.Text.Json.Serialization;

namespace IonPropeller.RemoteServices.Mapbox.Resources;

public class MapboxGeocodingResponse
{
    [JsonPropertyName("type")] public string Type { get; set; } = "";

    [JsonPropertyName("features")]
    public MapboxGeocodingFeature[] Features { get; set; } = Array.Empty<MapboxGeocodingFeature>();
}