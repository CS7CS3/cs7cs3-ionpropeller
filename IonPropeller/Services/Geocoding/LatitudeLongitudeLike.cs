using System.Text.Json.Serialization;

namespace IonPropeller.Services.Geocoding;

public class LatitudeLongitudeLike
{
    [JsonPropertyName("lat")] public double Latitude { get; set; }

    [JsonPropertyName("lng")] public double Longitude { get; set; }
}