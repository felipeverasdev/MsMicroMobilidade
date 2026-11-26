using Domain.Enums;

namespace Domain.Entities;

public class ParkingSpace
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public VehicleType AllowedVehicleType { get; private set; }
    public bool HasChargingPoint { get; private set; }
    public bool IsOccupied { get; private set; }
    public VehicleType? CurrentVehicleType { get; private set; }

    public ParkingSpace(string code, VehicleType allowedVehicleType, bool hasChargingPoint)
    {
        if(string.IsNullOrWhiteSpace(Code))
        {
            throw new ArgumentNullException("O código da vaga é obrigatório");
        }

        Id = Guid.NewGuid();
        Code = code.Trim().ToUpper();
        AllowedVehicleType = allowedVehicleType;
        HasChargingPoint = hasChargingPoint;
        IsOccupied = false;
        CurrentVehicleType = null;
    }

    public void Occupe(VehicleType incomingVehicle)
    {
        if (incomingVehicle == VehicleType.Shared)
            throw new InvalidOperationException("O tipo de um veículo específico não pode ser 'Misto'.");

        if (IsOccupied)
            throw new InvalidOperationException($"A vaga {Code} já está ocupada.");

        if (AllowedVehicleType != VehicleType.Shared && AllowedVehicleType != incomingVehicle)
            throw new InvalidOperationException($"Não é possível estacionar um veículo do tipo {incomingVehicle} na vaga {Code}, apenas {AllowedVehicleType}.");

        IsOccupied = true;
        CurrentVehicleType = incomingVehicle;
    }

    public void Release()
    {
        if (!IsOccupied)
            return;

        IsOccupied = false;
        CurrentVehicleType = null;
    }
}

