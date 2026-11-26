using Domain.Entities;

namespace Domain.Interfaces;

public interface IParkingSpaceRepository
{
    Task AddAsync(ParkingSpace parkingSpace);
    Task<bool> ExistsByCodeAsync(string code);
}
