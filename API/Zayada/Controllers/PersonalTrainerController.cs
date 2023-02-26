using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Net.Http.Headers;

namespace ZayadaAPI.Controllers
{
    public class PersonalTrainerController: BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public PersonalTrainerController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _hostingEnvironment = webHostEnvironment;
        }

        [HttpGet("personalTrainers")]
        public async Task<ActionResult<List<PersonalTrainer>>> GetAllPersonalTrainers()
        {
          return await _dataContext.PersonalTrainers.Include(x => x.Gym).ToListAsync();
        }

        [HttpPost("personalTrainers")]
        public async Task<ActionResult<List<PersonalTrainer>>> AddPersonalTrainer([FromQuery]PersonalTrainerDto personalTrainer)
        {
            if(personalTrainer == null) 
                return BadRequest();
            // map personal trainer dto to personal trainer
            var personalTrainerMap = new PersonalTrainer
            {
                Name = personalTrainer.Name,
                Email = personalTrainer.Email,
                InstagramLink = personalTrainer.InstagramLink,
                Description = personalTrainer.Description,
                ImageUrl = personalTrainer.ImageUrl,
                Certifications = personalTrainer.Certifications,
                GymId = personalTrainer.GymId
            };
               _dataContext.PersonalTrainers.Add(personalTrainerMap);
            await   _dataContext.SaveChangesAsync();
            return Ok(personalTrainer);
        }

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

    //to be removed

    public class PersonalTrainerDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? InstagramLink { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Certifications { get; set; }
        public int GymId { get; set; }
    }
}
