using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name)
    : ICommand<CategoryResponse>;
