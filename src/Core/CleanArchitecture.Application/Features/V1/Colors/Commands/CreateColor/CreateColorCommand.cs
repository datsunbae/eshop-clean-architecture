using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Colors.Commands.CreateColor;

public sealed record CreateColorCommand(string Name) : ICommand<Guid>;
