using MicroMob.Core.Models;
using MicroMob.Core.Interfaces;

namespace MicroMob.Core.Services;

public class OccupySpaceService
{
    private readonly IParkingSpaceRepository _repository;

    public OccupySpaceService(IParkingSpaceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ParkingSpaceResponse?> ExecuteAsync(Guid id, OccupySpaceRequest request)
    {
        var space = await _repository.GetByIdAsync(id);

        if (space == null)
            return null;

        space.Occupy(request.VehicleType);

        await _repository.UpdateAsync(space);

        return new ParkingSpaceResponse(
            space.Id, space.Code, space.AllowedVehicleType, space.HasChargingPoint, space.IsOccupied, space.CurrentVehicleType);
    }
}
