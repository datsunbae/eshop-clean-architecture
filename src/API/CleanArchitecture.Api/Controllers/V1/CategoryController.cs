using Asp.Versioning;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;
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

    [HttpPost("categories-paged")]
    [ProducesResponseType(typeof(Result<IReadOnlyList<CategoryResponse>>), StatusCodes.Status200OK)]
    [MustHavePermission(Action.View, Resource.Categories)]
    public async Task<ActionResult<PaginationResponse<CategoryResponse>>> GetCategorisPaged([FromBody] GetCategoriesQuery request)
    {
        var result = await Sender.Send(request);
        return Ok(result);
    }
}
