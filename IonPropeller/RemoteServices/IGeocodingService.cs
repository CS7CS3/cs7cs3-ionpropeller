namespace IonPropeller.RemoteServices;

public interface IGeocodingService
{
    /// <param name="address">Address to query</param>
    /// <param name="latitude">Latitude of position to search nearby</param>
    /// <param name="longitude">Longitude of position to search nearby</param>
    public Task<IEnumerable<GeocodingFeature>> QueryForward(string address, double latitude, double longitude);

    /// <param name="latitude">Latitude of position to search nearby (e.g. user or pinned location)</param>
    /// <param name="longitude">Longitude of position to search nearby (e.g. user or pinned location)</param>
    public Task<IEnumerable<GeocodingFeature>> QueryReverse(double latitude, double longitude);
}