using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications.PersonalTrainers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Net.Http.Headers;
using ZayadaAPI.Dtos;
using ZayadaAPI.Helpers;

namespace ZayadaAPI.Controllers
{
    public class PersonalTrainerController: BaseApiController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IGenericRepository<PersonalTrainer> _personalTrainerRepository;
        private readonly IMapper _mapper;
        public PersonalTrainerController(IWebHostEnvironment webHostEnvironment, IGenericRepository<PersonalTrainer> personalTrainerRepo,IMapper mapper)
        {
            _hostingEnvironment = webHostEnvironment;
            _personalTrainerRepository = personalTrainerRepo;
            _mapper = mapper;
        }

        [HttpGet("personalTrainers")]
        public async Task<ActionResult<Pagination<PersonalTrainersToReturnDto>>> GetTrainers([FromQuery] PersonalTrainersParam personalTrainersParam)
        {
            var spec = new PersonalTrainersSpecification(personalTrainersParam);
            var countSpec = new PersonalTrainersWithFilterForCountSpecification(personalTrainersParam);
            var totalItems = await _personalTrainerRepository.CountAsync(countSpec);
            var trainers = await _personalTrainerRepository.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<PersonalTrainer>,IReadOnlyList<PersonalTrainersToReturnDto>>(trainers);
            if(trainers.Count == 0)
            {
                return NotFound(404);
            }
            return Ok(new Pagination<PersonalTrainersToReturnDto>(personalTrainersParam.PageIndex,personalTrainersParam.PageSize,totalItems,data));
        }
        
        [HttpPost("personalTrainers")]
        public async Task<ActionResult<PersonalTrainersToPost>> AddTrainer([FromQuery] PersonalTrainersToPost personalTrainer)
        {
          var newTrainer = _mapper.Map<PersonalTrainersToPost, PersonalTrainer>(personalTrainer);
            if(string.IsNullOrEmpty(newTrainer.Name))
            {
                return BadRequest(400);
            }
            await _personalTrainerRepository.AddAsync(newTrainer);
            return Ok(newTrainer);

        }
/*
        [HttpGet("gyms")]

        public async Task<ActionResult<List<Gym>>> GetAllGyms()
        {
            var gyms = await _dataContext.Gyms.ToListAsync();
            if(gyms == null)
                return BadRequest();
            return    Ok(gyms);
        }

        [HttpPost("gyms")]
        public async Task<ActionResult<List<Gym>>> AddGym([FromQuery]Gym gym)
        {
            if(gym == null)
                return BadRequest();
            await _dataContext.Gyms.AddAsync(gym);
            await _dataContext.SaveChangesAsync();
            return Ok(gym);
        }
        */
        [HttpPost("uploadTrainerProfileImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (!file.ContentType.Contains("image"))
                {
                    return BadRequest("The file is not an image");
                }
                if (!file.FileName.EndsWith(".png") && !file.FileName.EndsWith(".jpg"))
                {
                    return BadRequest("The file extension is not png or jpg");
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName
                        .Trim('"');
                    string fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "Files/Images", fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return Ok(new { fileName });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
  
    }
    ////////
}
