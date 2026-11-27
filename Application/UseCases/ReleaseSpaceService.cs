using Application.DTOs;
using Domain.Interfaces;

namespace Application.UseCases;

public class ReleaseSpaceService
{
    private readonly IParkingSpaceRepository _repository;

    public ReleaseSpaceService(IParkingSpaceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ParkingSpaceResponse?> ExecuteAsync(Guid id)
    {
        var space = await _repository.GetByIdAsync(id);

        if (space is null)
            return null;

        space.Release();

        await _repository.UpdateAsync(space);

        return new ParkingSpaceResponse(
            space.Id, space.Code, space.AllowedVehicleType, space.HasChargingPoint, space.IsOccupied, space.CurrentVehicleType);
    }
}
