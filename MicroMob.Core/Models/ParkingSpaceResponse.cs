using MicroMob.Core.Domain.Enums;

namespace MicroMob.Core.Models;

public record ParkingSpaceResponse(
    Guid Id,
    string Code,
    VehicleType AllowedVehicleType,
    bool HasChargingPoint,
    bool IsOccupied,
    VehicleType? CurrentVehicleType
    );
