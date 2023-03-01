using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications.PersonalTrainers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Net.Http.Headers;
using ZayadaAPI.Dtos;
using ZayadaAPI.Helpers;

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
            var spec = new PersonalTrainersSpecification(personalTrainersParam);
            var countSpec = new PersonalTrainersWithFilterForCountSpecification(personalTrainersParam);
            var totalItems = await PersonalTrainerRepository.CountAsync(countSpec);
            var trainers = await PersonalTrainerRepository.ListAsync(spec);
            var data = Mapper.Map<IReadOnlyList<PersonalTrainer>,IReadOnlyList<PersonalTrainersToReturnDto>>(trainers);
            if(trainers.Count == 0)
            {
                return NotFound(404);
            }
            return Ok(new Pagination<PersonalTrainersToReturnDto>(personalTrainersParam.PageIndex,personalTrainersParam.PageSize,totalItems,data));
        }
        
        [HttpPost]
        public async Task<ActionResult<PersonalTrainersToPost>> AddTrainer([FromQuery] PersonalTrainersToPost personalTrainer)
        {
          var newTrainer = Mapper.Map<PersonalTrainersToPost, PersonalTrainer>(personalTrainer);
            if(string.IsNullOrEmpty(newTrainer.Name))
            {
                return BadRequest(400);
            }
            await PersonalTrainerRepository.AddAsync(newTrainer);
            return Ok(newTrainer);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalTrainer>> GetTrainerById(int id)
        {
            var spec = new PersonalTrainersSpecification(id);
            var trainer = await PersonalTrainerRepository.GetEntityWithSpec(spec);
            if(trainer == null)
            {
                return NotFound(404);
            }
            return Ok(Mapper.Map<PersonalTrainer,PersonalTrainersToReturnDto>(trainer));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrainer(int id)
        {
            var spec = new PersonalTrainersSpecification(id);
            var trainer = await PersonalTrainerRepository.GetEntityWithSpec(spec);
            if(trainer == null)
            {
                return NotFound(404);
            }
            await PersonalTrainerRepository.DeleteAsync(spec);
            return Ok(200);
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

}
