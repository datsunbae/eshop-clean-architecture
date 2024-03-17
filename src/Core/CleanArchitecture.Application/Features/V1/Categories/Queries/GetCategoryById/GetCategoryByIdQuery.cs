using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) 
    : IQuery<CategoryResponse>;
