using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Application.Features.V1.Colors.Specs;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Colors.Queries.GetColors;

public sealed class GetColorsQueryHandler : IQueryHandler<GetColorsQuery, PaginationResponse<ColorResponse>>
{
    private readonly IColorRepository _colorRepository;
    public GetColorsQueryHandler(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository ?? throw new ArgumentNullException(nameof(colorRepository));
    }

    public async Task<Result<PaginationResponse<ColorResponse>>> Handle(GetColorsQuery request, CancellationToken cancellationToken)
    {
        var spec = new ColorsPaginatedSpec(request);
        var result = await _colorRepository.PaginatedListAsync(spec, request.PageSize, request.PageNumber, cancellationToken);
        return result;
    }
}
