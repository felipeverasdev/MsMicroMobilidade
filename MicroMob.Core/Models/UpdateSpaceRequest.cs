using MicroMob.Core.Domain.Enums;

namespace MicroMob.Core.Models;

public record UpdateSpaceRequest(
    string Code,
    VehicleType AllowedVehicleType,
    bool HasChargingPoint
);
