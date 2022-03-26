using System.Text.Json.Serialization;
using IonPropeller.Services.Geocoding;

namespace IonPropeller.Services.Journey;

public class JourneyGroup
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;

    [JsonPropertyName("host")] public JourneyParticipant Host { get; set; } = new();

    [JsonPropertyName("guests")] public List<JourneyParticipant> Guests { get; set; } = new();

    [JsonPropertyName("destination")] public GeocodingFeature Destination { get; set; } = new();

    [JsonPropertyName("origin")] public GeocodingFeature Origin { get; set; } = new();
}