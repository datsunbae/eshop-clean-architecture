using Asp.Versioning;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Categories.Commands.CreateCategory;
using CleanArchitecture.Application.Features.V1.Categories.Commands.DeleteCategory;
using CleanArchitecture.Application.Features.V1.Categories.Commands.UpdateCategory;
using CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;
using CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategoryById;
using CleanArchitecture.Application.Features.V1.Categories;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Identity.Auth.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Features.V1.Colors;
using CleanArchitecture.Application.Features.V1.Colors.Queries.GetColors;
using CleanArchitecture.Application.Features.V1.Colors.Commands.CreateColor;
using CleanArchitecture.Application.Features.V1.Colors.Commands.UpdateColor;
using CleanArchitecture.Application.Features.V1.Colors.Commands.DeleteColor;

namespace CleanArchitecture.Api.Controllers.V1
{
    [ApiVersion(1)]
    public class ColorController : BaseApiController
    {
        public ColorController(ISender sender) : base(sender)
        {
        }

        /// <summary>
        /// Get the paginated color list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("paginated")]
        [ProducesResponseType(typeof(Result<PaginationResponse<ColorResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaginatedColors([FromBody] GetColorsQuery request)
        {
            var result = await Sender.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Create color
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Result<ColorResponse>), StatusCodes.Status200OK)]
        [MustHavePermission(Action.Create, Resource.Categories)]
        public async Task<IActionResult> CreateColor([FromBody] CreateColorCommand request)
        {
            var result = await Sender.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Update color
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Result<ColorResponse>), StatusCodes.Status200OK)]
        [MustHavePermission(Action.Update, Resource.Categories)]
        public async Task<IActionResult> UpdateColor(Guid id, [FromBody] UpdateColorCommand request)
        {
            if (request.Id != id)
                return BadRequest();

            var result = await Sender.Send(request);

            if (result.IsFailure)
                throw new BadRequestException(new List<Error> { result.Error });
            
            return Ok(result);
        }

        /// <summary>
        /// Delete color
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [MustHavePermission(Action.Delete, Resource.Categories)]
        public async Task<IActionResult> DeleteColor(Guid id)
        {
            var result = await Sender.Send(new DeleteColorCommand(id));

            if (result.IsFailure)
                throw new BadRequestException(new List<Error> { result.Error });

            return Ok(result);
        }
    }
}
