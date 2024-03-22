using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Features.V1.Colors.Queries.GetColors;

public sealed class GetColorsQuery : PaginationFilter, IQuery<PaginationResponse<ColorResponse>>
{
}
