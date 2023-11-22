using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Model.Common;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public CustomBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [NonAction]
        public IActionResult CreateActionResult<T>(ApiResponse<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null) { StatusCode = response.StatusCode };
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

    }
}
