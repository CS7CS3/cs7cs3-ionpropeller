using IonPropeller.Services.Geocoding;

namespace IonPropeller.Services.Journey;

public interface IGroupService
{
    public Task<JourneyGroup> GetGroup(Guid id);

    public Task<IEnumerable<JourneyGroup>> GetNearbyGroups(LatitudeLongitudeLike destinationPosition,
        LatitudeLongitudeLike originPosition);
}