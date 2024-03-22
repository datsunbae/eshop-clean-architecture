using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Colors.Commands.UpdateColor;

public sealed record UpdateColorCommand(Guid Id, string Name) : ICommand<Guid>
{
}
