using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryParkingSpaceRepository : IParkingSpaceRepository
{
    private readonly List<ParkingSpace> _spacesList = [];

    public Task AddAsync(ParkingSpace parkingSpace)
    {
        _spacesList.Add(parkingSpace);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsByCodeAsync(string code)
    {
        var exists = _spacesList.Any(s => s.Code == code);
        return Task.FromResult(exists);
    }
}
