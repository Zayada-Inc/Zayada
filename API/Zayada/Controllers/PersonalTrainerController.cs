using Application.Dtos;
using Application.PersonalTrainers;
using BrianMed.SmartCrop;
using Domain.Entities;
using Domain.Helpers;
using Domain.Specifications.PersonalTrainers;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Net.Http.Headers;

namespace ZayadaAPI.Controllers
{
    public class PersonalTrainerController: BaseApiController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public PersonalTrainerController(IWebHostEnvironment webHostEnvironment)
        {
            _hostingEnvironment = webHostEnvironment;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<PersonalTrainersToReturnDto>>> GetTrainers([FromQuery] PersonalTrainersParam personalTrainersParam)
        {
            var trainers = await Mediator.Send(new PersonalTrainersList.Query { PersonalTrainerParams = personalTrainersParam });
            if(trainers.Data.Count == 0)
            {
                return NotFound(404);
            }

            return Ok(trainers);
        }
        
        [HttpPost]
        public async Task<ActionResult<PersonalTrainersToPost>> AddTrainer([FromQuery] PersonalTrainersToPost personalTrainer)
        {      
            if(personalTrainer == null)
            {
                return BadRequest(400);
            }

            await Mediator.Send(new PersonalTrainerCreate.Command { PersonalTrainer = personalTrainer });

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalTrainersToReturnDto>> GetTrainerById(int id)
        {
           var trainer = await Mediator.Send(new PersonalTrainerById.Query { Id = id });
            if(trainer == null)
            {
                return NotFound(404);
            }
            return Ok(trainer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrainer(int id)
        {
            if(id >= 0)
            {
                await Mediator.Send(new PersonalTrainerDelete.Command { Id = id });
                return Ok(200);
            }
            return BadRequest();
        }
       
        [HttpPost("uploadTrainerProfileImageSmartCrop")]
        public async Task<IActionResult> UploadImageSmartCrop(IFormFile file)
        {
            try
            {
                if (!file.ContentType.Contains("image"))
                {
                    return BadRequest("The file is not an image");
                }
                if (!file.FileName.EndsWith(".png") && !file.FileName.EndsWith(".jpg") && !file.FileName.EndsWith(".jpeimageg"))
                {
                    return BadRequest("The file extension is not png or jpg");
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName
                        .Trim('"');
                    string fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "Files/Images", fileName);

                    var result = new ImageCrop(200, 200).Crop(file.OpenReadStream());

                        using(Image image = Image.Load(file.OpenReadStream()))
                        {

                            image.Mutate(x => x.Crop(result.Area));
                            await image.SaveAsPngAsync(fullPath);
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

        [HttpPost("uploadTrainerProfileImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (!file.ContentType.Contains("image"))
                {
                    return BadRequest("The file is not an image");
                }
                if (!file.FileName.EndsWith(".png") && !file.FileName.EndsWith(".jpg") || file.FileName.EndsWith(".jpg"))
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

}
