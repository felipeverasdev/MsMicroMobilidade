using Domain.Enums;

namespace Application.DTOs;

public record ParkingSpaceResponse(
    Guid Id,
    string Code,
    VehicleType AllowedVehicleType,
    bool HasChargingPoint,
    bool IsOccupied,
    VehicleType? CurrentVehicleType
    );
