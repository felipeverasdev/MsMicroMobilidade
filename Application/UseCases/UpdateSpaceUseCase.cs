using Application.DTOs;
using Domain.Interfaces;

namespace Application.UseCases;

public class UpdateSpaceUseCase
{
    private readonly IParkingSpaceRepository _repository;

    public UpdateSpaceUseCase(IParkingSpaceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ParkingSpaceResponse?> ExecuteAsync(Guid id, UpdateSpaceRequest request)
    {
        var space = await _repository.GetByIdAsync(id);

        if (space == null)
            return null;

        var normalizedCode = request.Code?.Trim().ToUpper() ?? string.Empty;

        if (await _repository.ExistsByCodeExcludingIdAsync(normalizedCode, id))
            throw new InvalidOperationException($"Já existe outra vaga cadastrada com o código '{normalizedCode}'.");

        space.UpdateDetails(normalizedCode, request.AllowedVehicleType, request.HasChargingPoint);

        await _repository.UpdateAsync(space);

        return new ParkingSpaceResponse(
            space.Id, space.Code, space.AllowedVehicleType, space.HasChargingPoint, space.IsOccupied, space.CurrentVehicleType);
    }
}
