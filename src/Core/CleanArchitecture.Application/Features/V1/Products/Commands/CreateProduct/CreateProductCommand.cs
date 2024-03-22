using CleanArchitecture.Application.Common.FileStorage;
using CleanArchitecture.Application.Common.Messaging;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.V1.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    FileUploadRequest? Image,
    Guid CategoryId)
    : ICommand<Guid>;
