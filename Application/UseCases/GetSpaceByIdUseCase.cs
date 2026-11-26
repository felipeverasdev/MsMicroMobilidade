using Application.DTOs;
using Domain.Interfaces;

namespace Application.UseCases;

public class GetSpaceByIdUseCase
{
    private readonly IParkingSpaceRepository _repository;

    public GetSpaceByIdUseCase(IParkingSpaceRepository repository)
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
