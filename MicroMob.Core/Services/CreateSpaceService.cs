using MicroMob.Core.Models;
using MicroMob.Core.Domain.Entities;
using MicroMob.Core.Models.Exceptions;
using MicroMob.Core.Interfaces;

namespace MicroMob.Core.Services;

public class CreateSpaceService
{
    private readonly IParkingSpaceRepository _repository;

    public CreateSpaceService(IParkingSpaceRepository repository)
    {
        _repository = repository;
    }

public async Task<ParkingSpaceResponse> ExecuteAsync(CreateParkingSpaceRequest request)
    {
        var normalizedCode = request.Code?.Trim().ToUpper() ?? string.Empty;

        if(await _repository.ExistsByCodeAsync(normalizedCode))
        {
            throw new ParkingSpaceException($"Já existe uma vaga cadastrada com o código '{normalizedCode}'.");
        }

        var parkingSpace = new ParkingSpace(
            normalizedCode,
            request.AllowedVehicleType,
            request.HasChargingPoint
            );

        await _repository.AddAsync(parkingSpace);

        return new ParkingSpaceResponse(
            parkingSpace.Id,
            parkingSpace.Code,
            parkingSpace.AllowedVehicleType,
            parkingSpace.HasChargingPoint,
            parkingSpace.IsOccupied,
            null
            );
    }
}
