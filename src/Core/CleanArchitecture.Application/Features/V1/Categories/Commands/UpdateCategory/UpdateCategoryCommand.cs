using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid Id, string Name) 
    : ICommand<Guid>
{
}
