using Asp.Versioning;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Features.V1.Baskets.Commands.AddBasketProductItem;
using CleanArchitecture.Application.Features.V1.Baskets.Commands.CheckoutBasket;
using CleanArchitecture.Application.Features.V1.Baskets.Commands.ClearBasket;
using CleanArchitecture.Application.Features.V1.Baskets.Commands.RemoveBasketProductItem;
using CleanArchitecture.Application.Features.V1.Baskets.Models.Responses;
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
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Result<BasketResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBasket()
        {
            var result = await Sender.Send(new GetBasketQuery());

            return Ok(result);
        }

        /// <summary>
        /// Add product into basket
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
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
        [HttpDelete("remove-product-item")]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveBasketProductItem([FromBody] RemoveBasketProductItemCommand request)
        {
            var result = await Sender.Send(request);

            if (result.IsFailure)
            {
                throw new BadRequestException(new List<Error> { result.Error });
            }

            return Ok(result);
        }

        /// <summary>
        /// Clear basket
        /// </summary>
        /// <returns></returns>
        [HttpDelete("clear-basket")]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ClearBasket()
        {
            var result = await Sender.Send(new ClearBasketCommand());
            return Ok(result);
        }

        /// <summary>
        /// Check out
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("check-out")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutCommand request)
        {
            var result = await Sender.Send(request);
            return Ok(result);
        }
    }
}
