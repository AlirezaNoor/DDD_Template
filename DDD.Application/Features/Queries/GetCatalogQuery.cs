using DDD.Application.Common;
using DDD.Domain.Common;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;

namespace DDD.Application.Features.Queries;

public record CatalogDto(Guid Id, string Name, string Description);

public record GetCatalogQuery(Guid Id) : IQuery<CatalogDto>;

public class GetCatalogQueryHandler : IQueryHandler<GetCatalogQuery, CatalogDto>
{
    private readonly IRepository<Catalog, CatalogId> _repository;

    public GetCatalogQueryHandler(IRepository<Catalog, CatalogId> repository)
    {
        _repository = repository;
    }

    public async Task<Result<CatalogDto>> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
    {
        var catalog = await _repository.GetByIdAsync(new CatalogId(request.Id), cancellationToken);

        if (catalog is null)
        {
            return Result.Failure<CatalogDto>(DDD.Domain.Common.Error.NotFound("Catalog.NotFound", $"Catalog with ID {request.Id} was not found."));
        }

        return new CatalogDto(catalog.Id.Value, catalog.Name, catalog.Description);
    }
}
