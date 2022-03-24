using System.Text.Json.Serialization;

namespace IonPropeller.RemoteServices.Mapbox.Resources;

public class MapboxGeocodingFeature
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("center")] public double[] Center { get; set; }

    [JsonPropertyName("place_name")] public string PlaceName { get; set; }

    [JsonPropertyName("relevance")] public double Relevance { get; set; }

    [JsonPropertyName("text")] public string Text { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }

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