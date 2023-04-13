using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ZayadaAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class MembershipController: ControllerBase
    {
        private readonly IGymMembershipService _gymMembershipService;
        private readonly IGenericRepository<Gym> _gymRepo;
        private readonly UserManager<AppUser> _userManager;

        public MembershipController(IGymMembershipService gymMembershipService, UserManager<AppUser> userManager,
            IGenericRepository<Gym> gymRepo)
        {
            _gymMembershipService = gymMembershipService;
            _userManager = userManager;
            _gymRepo = gymRepo;
        }

       

    }
}
