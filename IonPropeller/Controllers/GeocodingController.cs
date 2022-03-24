using IonPropeller.RemoteServices;
using Microsoft.AspNetCore.Mvc;

namespace IonPropeller.Controllers;

[ApiController]
[Route("api/v1/geocoding")]
public class GeocodingController
{
    private readonly IGeocodingService _geocodingService;

    public GeocodingController(IGeocodingService geocodingService)
    {
        _geocodingService = geocodingService;
    }

    /// <param name="search">Address or place name to search</param>
    /// <param name="latitude">Latitude of location to search around (e.g user location)</param>
    /// <param name="longitude">Longitude of location to search around (e.g user location)</param>
    [HttpGet("forward")]
    public async Task<IEnumerable<GeocodingFeature>> ForwardGeocode(string search, double latitude, double longitude)
    {
        return await _geocodingService.QueryForward(search, latitude, longitude);
    }

    /// <param name="latitude">Latitude of location to search around (e.g user or pinned location)</param>
    /// <param name="longitude">Longitude of location to search around (e.g user or pinned location)</param>
    [HttpGet("reverse")]
    public async Task<IEnumerable<GeocodingFeature>> ReverseGeocode(double latitude, double longitude)
    {
        return await _geocodingService.QueryReverse(latitude, longitude);
    }
}