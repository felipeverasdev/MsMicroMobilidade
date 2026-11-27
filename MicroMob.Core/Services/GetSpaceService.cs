using MicroMob.Core.Models;
using MicroMob.Core.Interfaces;

namespace MicroMob.Core.Services;

public class GetSpaceService
{
    private readonly IParkingSpaceRepository _repository;

    public GetSpaceService(IParkingSpaceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ParkingSpaceResponse?> ExecuteAsync(Guid id)
    {
        var space = await _repository.GetByIdAsync(id);

        if (space == null)
            return null;

        return new ParkingSpaceResponse(
            space.Id,
            space.Code,
            space.AllowedVehicleType,
            space.HasChargingPoint,
            space.IsOccupied,
            space.CurrentVehicleType
            );
    }
}
