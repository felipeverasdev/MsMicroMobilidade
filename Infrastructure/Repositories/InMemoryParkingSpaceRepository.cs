using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryParkingSpaceRepository : IParkingSpaceRepository
{
    private static readonly List<ParkingSpace> _spacesList = [];

    public Task AddAsync(ParkingSpace parkingSpace)
    {
        _spacesList.Add(parkingSpace);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsByCodeAsync(string code)
    {
        var result = _spacesList.Any(s => string.Equals(s.Code, code, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(result);
    }

    public Task<IEnumerable<ParkingSpace>> GetAllAsync()
    {
        IEnumerable<ParkingSpace> result = _spacesList.ToList();
        return Task.FromResult(result);
    }

    public Task<ParkingSpace?> GetByIdAsync(Guid id)
    {
        var result = _spacesList.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(result);
    }

    public Task UpdateAsync(ParkingSpace parkingSpace)
    {
        return Task.CompletedTask;
    }

    public Task<bool> ExistsByCodeExcludingIdAsync(string code, Guid excludeId)
    {
        var result = _spacesList.Any(s => string.Equals(s.Code, code,
            StringComparison.OrdinalIgnoreCase) && s.Id != excludeId);
        return Task.FromResult(result);
    }
}
