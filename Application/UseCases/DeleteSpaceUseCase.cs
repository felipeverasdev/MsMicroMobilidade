using Domain.Interfaces;

namespace Application.UseCases;

public class DeleteSpaceUseCase
{
    private readonly IParkingSpaceRepository _repository;

    public DeleteSpaceUseCase(IParkingSpaceRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var space = await _repository.GetByIdAsync(id);

        if (space is null)
            return false;

        if (space.IsOccupied)
            throw new InvalidOperationException($"Não é possível excluir uma vaga que está sendo ocupada.");

        await _repository.DeleteAsync(id);
        return true;
    }
}
