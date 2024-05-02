using CleanArchitecture.Application.Common.ApplicationServices.FileStorage;
using CleanArchitecture.Application.Common.Messaging;

namespace CleanArchitecture.Application.Features.V1.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    FileUploadRequest? Image,
    Guid CategoryId)
    : ICommand<Guid>;
