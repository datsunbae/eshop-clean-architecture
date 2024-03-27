using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Categories.Models.Responses;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) 
    : IQuery<CategoryResponse>;
