using DDD.Application.Common;
using DDD.Domain.Common;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;

namespace DDD.Application.Features.Commands;

public record CreateCatalogCommand(string Name, string Description) : ICommand<Guid>;

public class CreateCatalogCommandHandler : ICommandHandler<CreateCatalogCommand, Guid>
{
    private readonly IRepository<Catalog, CatalogId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCatalogCommandHandler(IRepository<Catalog, CatalogId> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = Catalog.Create(request.Name, request.Description);

        await _repository.AddAsync(catalog, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return catalog.Id.Value;
    }
}
