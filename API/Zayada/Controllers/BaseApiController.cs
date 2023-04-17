using Application.Interfaces;
using Application.Services;
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

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IUserAccesor UserAccessor => _userAccesor ??= HttpContext.RequestServices.GetService<IUserAccesor>();
        protected IGymService GymService => _gymService ??= HttpContext.RequestServices.GetService<IGymService>();
       
    }
}
