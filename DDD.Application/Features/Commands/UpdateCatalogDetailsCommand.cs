using DDD.Application.Common;
using DDD.Domain.Common;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;

namespace DDD.Application.Features.Commands;

public record UpdateCatalogDetailsCommand(Guid Id, string Name, string Description) : ICommand<Guid>;

public class UpdateCatalogDetailsCommandHandler : ICommandHandler<UpdateCatalogDetailsCommand, Guid>
{
    private readonly IRepository<Catalog, CatalogId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCatalogDetailsCommandHandler(IRepository<Catalog, CatalogId> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateCatalogDetailsCommand request, CancellationToken cancellationToken)
    {
        var catalog = await _repository.GetByIdAsync(new CatalogId(request.Id), cancellationToken);
        if (catalog is null)
        {
            return Result.Failure<Guid>(Error.NotFound("Catalog.NotFound", $"Catalog with ID {request.Id} was not found."));
        }

        catalog.UpdateDetails(request.Name, request.Description);
        _repository.Update(catalog);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return catalog.Id.Value;
    }
}
