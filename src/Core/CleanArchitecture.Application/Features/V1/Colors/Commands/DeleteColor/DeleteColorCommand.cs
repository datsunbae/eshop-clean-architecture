using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Colors.Commands.DeleteColor;

public sealed record DeleteColorCommand(Guid Id) : ICommand<Guid>
{
}
