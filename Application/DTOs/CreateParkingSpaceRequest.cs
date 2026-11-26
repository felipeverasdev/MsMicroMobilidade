using Domain.Enums;

namespace Application.DTOs;

public record CreateParkingSpaceRequest(
    string Code,
    VehicleType AllowedVehicleType,
    bool HasChargingPoint
    );
