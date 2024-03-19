using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : ICommand<Guid>;
