using CleanArchitecture.Application.Common.Messaging;
using System.Windows.Input;

namespace CleanArchitecture.Application.Features.V1.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId) : ICommand<Guid>;
