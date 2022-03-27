using Bogus;
using IonPropeller.Services.Geocoding;
using Microsoft.Extensions.Caching.Memory;

namespace IonPropeller.Services.Journey;

public class FakeGroupService : IGroupService
{
    private readonly IMemoryCache _cache;

    private readonly IGeocodingService _geocodingService;

    private readonly Faker<JourneyGroup> _groupFaker;

    public FakeGroupService(IMemoryCache cache, IGeocodingService geocodingService)
    {
        _cache = cache;
        _geocodingService = geocodingService;

        var participantFaker = new Faker<JourneyParticipant>()
            .RuleFor(p => p.Id, (f, _) => f.Random.Guid().ToString())
            .RuleFor(p => p.AvatarUrl, (f, _) => f.Internet.Avatar())
            .RuleFor(p => p.ScreenName, (f, _) => f.Internet.UserName());

        var groupType = new[] {"walk", "bus", "taxi"};
        _groupFaker = new Faker<JourneyGroup>()
            .RuleFor(g => g.Guests, (f, _) => participantFaker.Generate(f.Random.Int(1, 5)))
            .RuleFor(g => g.Host, (_, _) => participantFaker.Generate())
            .RuleFor(g => g.Id, (f, _) => Guid.NewGuid().ToString())
            .RuleFor(g => g.Type, (f, _) => f.PickRandom(groupType));
    }

    public Task<JourneyGroup> GetGroup(Guid id)
    {
        if (_cache.TryGetValue($"group:{id}", out JourneyGroup group))
            return Task.FromResult(group);
        throw new KeyNotFoundException($"Group with id {id} not found");
    }

    public async Task<IEnumerable<JourneyGroup>> GetNearbyGroups(GeocodingFeaturePosition destinationPosition,
        GeocodingFeaturePosition originPosition)
    {
        var cacheKey =
            $"group@{originPosition.Latitude},{originPosition.Longitude}>{destinationPosition.Latitude},{destinationPosition.Longitude}";

        if (_cache.TryGetValue(cacheKey, out JourneyGroup[] cachedResult)) return cachedResult;

        var destinations = await _geocodingService.QueryReverse(destinationPosition.Latitude,
            destinationPosition.Longitude, new[] {"poi"}, 10);
        var origins =
            await _geocodingService.QueryReverse(originPosition.Latitude, originPosition.Longitude, new[] {"poi"}, 10);

        var result = destinations.Zip(origins, (destination, origin) =>
        {
            var group = _groupFaker.Generate();
            group.Destination = destination;
            group.Origin = origin;

            _cache.Set($"group:{Guid.Parse(group.Id)}", group);
            return group;
        }).ToArray();

        _cache.Set(cacheKey, result);
        return result;
    }
}