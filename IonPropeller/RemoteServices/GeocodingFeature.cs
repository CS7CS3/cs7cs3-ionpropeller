using System.Text.Json.Serialization;

namespace IonPropeller.RemoteServices;

public class GeocodingFeature
{
    [JsonPropertyName("address")] public string Address { get; init; }

    [JsonPropertyName("displayName")] public string DisplayName { get; init; }

    [JsonPropertyName("position")] public GeocodingFeaturePosition Position { get; init; }

    [JsonPropertyName("relevance")] public double Relevance { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }
}