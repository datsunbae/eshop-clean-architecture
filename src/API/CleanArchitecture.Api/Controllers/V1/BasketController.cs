using Asp.Versioning;
using CleanArchitecture.Application.Features.V1.Baskets.Commands.AddBasketProductItem;
using CleanArchitecture.Application.Features.V1.Baskets.Commands.ClearBasket;
using CleanArchitecture.Application.Features.V1.Baskets.Commands.RemoveBasketProductItem;
using CleanArchitecture.Application.Features.V1.Baskets.Dtos;
using CleanArchitecture.Application.Features.V1.Baskets.Queries.GetBasket;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.V1
{
    [ApiVersion(1)]
    public class BasketController : BaseApiController
    {
        public BasketController(ISender sender) : base(sender)
        {
        }

        /// <summary>
        /// Get basket by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(Result<BasketReponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBasket(Guid userId)
        {
            var result = await Sender.Send(new GetBasketQuery(userId));
            return Ok(result);
        }

        /// <summary>
        /// Add product into basket
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBasketProductItem([FromBody] AddBasketProductItemCommand request)
        {
            var result = await Sender.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Remove product item in basket
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveBasketProductItem([FromBody] RemoveBasketProductItemCommand request)
        {
            var result = await Sender.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Clear basket
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ClearBasket([FromBody] ClearBasketCommand request)
        {
            var result = await Sender.Send(request);
            return Ok(result);
        }
    }
}
