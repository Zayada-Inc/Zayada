using Application.Interfaces;
using Application.Services;
using Infrastructure.Services.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ZayadaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController: ControllerBase
    {

        private  IMediator _mediator;
        private IUserAccesor _userAccesor;
        private IGymService _gymService;
        private IPaymentService _paymentService;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IUserAccesor UserAccessor => _userAccesor ??= HttpContext.RequestServices.GetService<IUserAccesor>();
        protected IGymService GymService => _gymService ??= HttpContext.RequestServices.GetService<IGymService>();
        protected IPaymentService PaymentService => _paymentService ??= HttpContext.RequestServices.GetService<IPaymentService>();
       
    }
}
