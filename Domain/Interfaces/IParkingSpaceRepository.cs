using Domain.Entities;

namespace Domain.Interfaces;

public interface IParkingSpaceRepository
{
    Task AddAsync(ParkingSpace parkingSpace);
    Task<bool> ExistsByCodeAsync(string code);
    Task<IEnumerable<ParkingSpace>> GetAllAsync();
    Task<ParkingSpace?> GetByIdAsync(Guid id);
    Task UpdateAsync(ParkingSpace parkingSpace);
    Task<bool> ExistsByCodeExcludingIdAsync(string code, Guid excludedId);
    Task DeleteAsync(Guid id);
}
