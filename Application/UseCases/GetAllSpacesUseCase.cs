using Application.DTOs;
using Domain.Interfaces;

namespace Application.UseCases;

public class GetAllSpacesUseCase
{
    private readonly IParkingSpaceRepository _repository;

    public GetAllSpacesUseCase(IParkingSpaceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ParkingSpaceResponse>> ExecuteAsync()
    {
        var spaces = await _repository.GetAllAsync();

        return spaces.Select(space => new ParkingSpaceResponse(
            space.Id,
            space.Code,
            space.AllowedVehicleType,
            space.HasChargingPoint,
            space.IsOccupied,
            space.CurrentVehicleType
            ));
    }
}
