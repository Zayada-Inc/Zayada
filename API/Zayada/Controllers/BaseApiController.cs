using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ZayadaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController: ControllerBase
    {
        private  IGenericRepository<PersonalTrainer> _personalTrainerRepository;
        private  IGenericRepository<Gym> _gymRepository;
        private  IMapper _mapper;

        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
        protected IGenericRepository<PersonalTrainer> PersonalTrainerRepository => _personalTrainerRepository ??= HttpContext.RequestServices.GetService<IGenericRepository<PersonalTrainer>>();
        protected IGenericRepository<Gym> GymRepository => _gymRepository ??= HttpContext.RequestServices.GetService<IGenericRepository<Gym>>();

       
    }
}
