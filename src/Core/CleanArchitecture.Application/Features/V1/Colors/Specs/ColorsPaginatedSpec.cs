using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Specification;
using CleanArchitecture.Domain.Colors;

namespace CleanArchitecture.Application.Features.V1.Colors.Specs;

public sealed class ColorsPaginatedSpec : EntitiesByPaginationFilterSpec<Color, ColorResponse>
{
    public ColorsPaginatedSpec(PaginationFilter filter) : base(filter)
    {
    }
}
