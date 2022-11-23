using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.OutBoxPatternInMicroServices.Application.Features.Commands.CreateOrder;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> OrderCreate(CreateOrderCommandRequest createOrderCommandRequest)
        {
            return Ok(await _mediator.Send(createOrderCommandRequest));
        }
    }
}
