using Domain.Enums;

namespace Application.DTOs;

public record UpdateSpaceRequest(
    string Code,
    VehicleType AllowedVehicleType,
    bool HasChargingPoint
);