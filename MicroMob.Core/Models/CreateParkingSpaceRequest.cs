using MicroMob.Core.Domain.Enums;

namespace MicroMob.Core.Models;

public record CreateParkingSpaceRequest(
    string Code,
    VehicleType AllowedVehicleType,
    bool HasChargingPoint
    );
