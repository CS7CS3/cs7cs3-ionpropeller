using System.Text.Json.Serialization;

namespace IonPropeller.Services.Journey;

public class JourneyParticipant
{
    [JsonPropertyName("id")] public string Id { get; set; } = Guid.Empty.ToString();

    [JsonPropertyName("avatarUrl")] public string AvatarUrl { get; set; } = string.Empty;

    [JsonPropertyName("screenName")] public string ScreenName { get; set; } = string.Empty;
}