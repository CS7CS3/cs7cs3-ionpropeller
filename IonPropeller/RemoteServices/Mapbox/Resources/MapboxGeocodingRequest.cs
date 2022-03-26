using IonPropeller.Services.Geocoding;

namespace IonPropeller.RemoteServices.Mapbox.Resources;

public class MapboxGeocodingRequest
{
    public static readonly string[] DefaultRequestTypes = {"place", "postcode", "address", "poi"};

    public bool AutoComplete { get; set; } = true;

    public bool FuzzyMatch { get; set; } = true;

    public uint? Limit { get; set; } = 10;

    public GeocodingFeaturePosition? Position { get; set; }

    public string[] Types { get; set; } = DefaultRequestTypes;

    public IReadOnlyDictionary<string, string> GetQueryParameters()
    {
        var result = new Dictionary<string, string>
        {
            {"autocomplete", AutoComplete.ToString().ToLower()},
            {"fuzzyMatch", FuzzyMatch.ToString().ToLower()},
            {"types", string.Join(",", Types)}
        };

        if (Limit is { } limit) result.Add("limit", limit.ToString());

        if (Position is not null) result.Add("proximity", $"{Position.Longitude},{Position.Latitude}");

        return result;
    }
}