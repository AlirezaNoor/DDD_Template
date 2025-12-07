using DDD.Application.Common;
using DDD.Domain.Common;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;

namespace DDD.Application.Features.Commands;

public record AddCatalogItemCommand(Guid CatalogId, string Name, decimal Amount, string Currency) : ICommand<Guid>;

public class AddCatalogItemCommandHandler : ICommandHandler<AddCatalogItemCommand, Guid>
{
    private readonly IRepository<Catalog, CatalogId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCatalogItemCommandHandler(IRepository<Catalog, CatalogId> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddCatalogItemCommand request, CancellationToken cancellationToken)
    {
        var catalog = await _repository.GetByIdAsync(new CatalogId(request.CatalogId), cancellationToken);
        if (catalog is null)
        {
            return Result.Failure<Guid>(Error.NotFound("Catalog.NotFound", $"Catalog with ID {request.CatalogId} was not found."));
        }

        catalog.AddItem(request.Name, request.Amount, request.Currency);
        _repository.Update(catalog);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var createdItemId = catalog.Items.Last().Id.Value;
        return createdItemId;
    }
}
