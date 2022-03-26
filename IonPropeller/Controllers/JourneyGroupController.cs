using System.ComponentModel.DataAnnotations;
using IonPropeller.Services.Geocoding;
using IonPropeller.Services.Journey;
using Microsoft.AspNetCore.Mvc;

namespace IonPropeller.Controllers;

[ApiController]
[Route("api/v1/journeys")]
public class JourneyGroupController : Controller
{
    private readonly IGroupService _groupService;

    public JourneyGroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet("groups/{id}")]
    public async Task<IActionResult> GetGroup([Required] Guid id)
    {
        if (!ModelState.IsValid) return BadRequest();

        try
        {
            return Ok(await _groupService.GetGroup(id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("groups")]
    public async Task<IActionResult> GetNearbyGroups(
        [Required] [FromQuery(Name = "destLat")]
        double destinationLatitude,
        [Required] [FromQuery(Name = "destLng")]
        double destinationLongitude,
        [Required] [FromQuery(Name = "originLat")]
        double originLatitude,
        [Required] [FromQuery(Name = "originLng")]
        double originLongitude)
    {
        if (!ModelState.IsValid) return BadRequest();

        return Ok(await _groupService.GetNearbyGroups(
            new GeocodingFeaturePosition
            {
                Latitude = destinationLatitude,
                Longitude = destinationLongitude
            },
            new GeocodingFeaturePosition
            {
                Latitude = originLatitude,
                Longitude = originLongitude
            }
        ));
    }
}