using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.V1
{
    [ApiVersion(1)]
    public class OrderController : BaseApiController
    {
        public OrderController(ISender sender) : base(sender)
        {
        }


    }
}
