using System.Text.Json.Serialization;

namespace IonPropeller.Services.Geocoding;

public class GeocodingFeature
{
    [JsonPropertyName("address")] public string Address { get; init; } = string.Empty;

    [JsonPropertyName("displayName")] public string DisplayName { get; init; } = string.Empty;

    [JsonPropertyName("position")]
    public GeocodingFeaturePosition Position { get; init; } = new() {Latitude = 0.0, Longitude = 0.0};

    [JsonPropertyName("relevance")] public double Relevance { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
}