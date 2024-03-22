using CleanArchitecture.Application.Common.FileStorage;
using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    FileUploadRequest? Image,
    Guid CategoryId) : ICommand<Guid>;
