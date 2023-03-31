using Application.Services.Photos.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Photos.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);
    }
}
