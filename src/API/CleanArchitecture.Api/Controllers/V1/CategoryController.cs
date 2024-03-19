using Asp.Versioning;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Categories;
using CleanArchitecture.Application.Features.V1.Categories.Commands.CreateCategory;
using CleanArchitecture.Application.Features.V1.Categories.Commands.DeleteCategory;
using CleanArchitecture.Application.Features.V1.Categories.Commands.UpdateCategory;
using CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;
using CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategoryById;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Identity.Auth.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.V1;

[ApiVersion(1)]
public class CategoryController : BaseApiController
{
    public CategoryController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Get the paginated category list
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("paginated")]
    [ProducesResponseType(typeof(Result<PaginationResponse<CategoryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaginatedCategories([FromBody] GetCategoriesQuery request)
    {
        var result = await Sender.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// Get category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<CategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        Result<CategoryResponse> result = await Sender.Send(new GetCategoryByIdQuery(id));

        if(result.IsFailure)
        {
            throw new BadRequestException(new List<Error> { result.Error });
        }

        return Ok(result);
    }

    /// <summary>
    /// Create category
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<CategoryResponse>), StatusCodes.Status200OK)]
    [MustHavePermission(Action.Create, Resource.Categories)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand request)
    {
        var result = await Sender.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// Update category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<CategoryResponse>), StatusCodes.Status200OK)]
    [MustHavePermission(Action.Update, Resource.Categories)]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand request)
    {
        if(request.Id != id)
            return BadRequest();

        var result = await Sender.Send(request);

        return CreatedAtAction(nameof(GetCategoryById), new { id = result.Value }, result.Value);
    }

    /// <summary>
    /// Delete category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [MustHavePermission(Action.Delete, Resource.Categories)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        return Ok(await Sender.Send(new DeleteCategoryCommand(id)));
    }
}
