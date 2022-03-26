using System.Text.Json.Serialization;

namespace IonPropeller.Services.Geocoding;

public class GeocodingFeaturePosition
{
    [JsonPropertyName("lat")] public double Latitude { get; set; }

    [JsonPropertyName("lng")] public double Longitude { get; set; }
}