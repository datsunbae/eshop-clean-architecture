using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Categories.Models.Responses;

namespace CleanArchitecture.Application.Features.V1.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name)
    : ICommand<CategoryResponse>;
