using Asp.Versioning;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Orders.Models.Responses;
using CleanArchitecture.Application.Features.V1.Orders.Queries.GetOrderByUserId;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.V1
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
    }
}
