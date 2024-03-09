using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Common;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected BaseApiController(ISender sender) => Sender = sender;
}
