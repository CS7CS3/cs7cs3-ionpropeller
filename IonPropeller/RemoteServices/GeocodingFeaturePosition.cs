using System.Text.Json.Serialization;

namespace IonPropeller.RemoteServices;

public class GeocodingFeaturePosition
{
    [JsonPropertyName("lat")] public double Latitude { get; set; }

    [JsonPropertyName("lng")] public double Longitude { get; set; }
}