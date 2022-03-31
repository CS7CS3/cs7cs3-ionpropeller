using System.Text.RegularExpressions;
using IonPropeller.Services.Directions;
using IonPropeller.Services.Geocoding;
using Microsoft.AspNetCore.Mvc;

namespace IonPropeller.Controllers;

[ApiController]
[Route("api/v1/directions")]
public class DirectionController : Controller
{
    private readonly IDirectionService _directionService;

    public DirectionController(IDirectionService directionService)
    {
        _directionService = directionService;
    }

    [HttpGet("{profile}/{waypoints}")]
    public async Task<ActionResult> Get(
        [FromRoute(Name = "profile")] string profileString,
        [FromRoute(Name = "waypoints")] string waypointString)
    {
        DirectionProfile? profileNullable = profileString switch
        {
            "driving" => DirectionProfile.Driving,
            "walking" => DirectionProfile.Walking,
            _ => null
        };
        if (profileNullable is not { } profile) return BadRequest("Invalid direction profile");

        var waypoints = waypointString.Split(";").ToList();
        if (waypoints.Count != 2) return BadRequest("Invalid waypoint number");

        var origin = ParseWaypointString(waypoints[0]);
        var destination = ParseWaypointString(waypoints[1]);

        if (origin is null || destination is null) return BadRequest("Invalid waypoint format");

        return Ok(await _directionService.GetDirections(origin, destination, profile));
    }

    private static LatitudeLongitudeLike? ParseWaypointString(string position)
    {
        var match = Regex.Match(position, @"^([-,+]?\d+(\.\d+)?),([-,+]?\d+(\.\d+)?)$");
        if (match.Success)
            return new LatitudeLongitudeLike
            {
                Latitude = double.Parse(match.Groups[3].Value),
                Longitude = double.Parse(match.Groups[1].Value)
            };
        return null;
    }
}