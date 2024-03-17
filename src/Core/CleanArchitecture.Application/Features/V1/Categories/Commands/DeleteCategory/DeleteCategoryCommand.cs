using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Categories.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid Id) 
    : ICommand<Guid>
{
}
