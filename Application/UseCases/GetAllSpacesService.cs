using Application.DTOs;
using Domain.Interfaces;

namespace Application.UseCases;

public class GetAllSpacesService
{
    private readonly IParkingSpaceRepository _repository;

    public GetAllSpacesService(IParkingSpaceRepository repository)
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
