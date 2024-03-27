using Asp.Versioning;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.V1.Products.Commands.DeleteProduct;
using CleanArchitecture.Application.Features.V1.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Features.V1.Products.Models.Responses;
using CleanArchitecture.Application.Features.V1.Products.Queries.GetProductById;
using CleanArchitecture.Application.Features.V1.Products.Queries.GetProducts;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Identity.Auth.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.V1;

[ApiVersion(1)]
public class ProductController : BaseApiController
{
    public ProductController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Get the paginated product list
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("paginated")]
    [ProducesResponseType(typeof(Result<PaginationResponse<ProductResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductPaginated([FromBody] GetProductsQuery request)
    {
        var result = await Sender.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var result = await Sender.Send(new GetProductByIdQuery(id));

        if (result.IsFailure)
            throw new BadRequestException(new List<Error> { result.Error });

        return Ok(result);
    }

    /// <summary>
    /// Create product
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<ProductResponse>), StatusCodes.Status200OK)]
    [MustHavePermission(Action.Create, Resource.Products)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
    {
        var result = await Sender.Send(request);

        if (result.IsFailure)
            throw new BadRequestException(new List<Error> { result.Error });

        return CreatedAtAction(nameof(GetProductById), new { Id = result.Value }, result.Value);
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(Result<ProductResponse>), StatusCodes.Status200OK)]
    [MustHavePermission(Action.Update, Resource.Products)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand request)
    {
        var result = await Sender.Send(request);

        if (result.IsFailure)
            throw new BadRequestException(new List<Error> { result.Error });

        return CreatedAtAction(nameof(GetProductById), new { Id =  result.Value}, result.Value);
    }

    /// <summary>
    /// Delete product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BadRequestException"></exception>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [MustHavePermission(Action.Delete, Resource.Products)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await Sender.Send(new DeleteProductCommand(id));

        if (result.IsFailure)
            throw new BadRequestException(new List<Error> { result.Error });

        return Ok(result);
    }
}
