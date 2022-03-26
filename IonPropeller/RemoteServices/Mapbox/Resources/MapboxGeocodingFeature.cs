using System.Text.Json.Serialization;
using IonPropeller.Services.Geocoding;

namespace IonPropeller.RemoteServices.Mapbox.Resources;

public class MapboxGeocodingFeature
{
    [JsonPropertyName("id")] public string Id { get; set; } = Guid.Empty.ToString();

    [JsonPropertyName("center")] public double[] Center { get; set; } = {0.0, 0.0};

    [JsonPropertyName("place_name")] public string PlaceName { get; set; } = string.Empty;

    [JsonPropertyName("relevance")] public double Relevance { get; set; } = 0.0;

    [JsonPropertyName("text")] public string Text { get; set; } = string.Empty;

    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;

    public static implicit operator GeocodingFeature(MapboxGeocodingFeature feature)
    {
        return new GeocodingFeature
        {
            Address = feature.PlaceName,
            DisplayName = feature.Text,
            Position = new GeocodingFeaturePosition {Latitude = feature.Center[1], Longitude = feature.Center[0]},
            Relevance = feature.Relevance,
            Type = feature.Type
        };
    }
}