using Asp.Versioning;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Orders.Models.Responses;
using CleanArchitecture.Application.Features.V1.Orders.Queries.GetOrderById;
using CleanArchitecture.Application.Features.V1.Orders.Queries.GetOrderByUserId;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.V1.Orders
{
    [ApiVersion(1)]
    public class OrderController : BaseApiController
    {
        public OrderController(ISender sender) : base(sender)
        {
        }

        /// <summary>
        /// Get orders by user id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Result<PaginationResponse<OrderResponse>>), StatusCodes.Status200OK)]
        public async Task<Result<PaginationResponse<OrderResponse>>> GetOrderByUserId([FromBody] GetOrderByUserIdQuery request)
        {
            var result = await Sender.Send(request);
            return result;
        }

        /// <summary>
        /// Get order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        [HttpGet]
        [ProducesResponseType(typeof(Result<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Result<OrderResponse>> GetOrderById(Guid id)
        {
            var result = await Sender.Send(new GetOrderByIdQuery(id));

            if (result.IsFailure)
            {
                throw new BadRequestException(new List<Error> { result.Error });
            }

            return result;
        }
    }
}
