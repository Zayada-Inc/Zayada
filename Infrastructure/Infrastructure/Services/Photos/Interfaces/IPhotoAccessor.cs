using Infrastructure.Services.Photos.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Photos.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);
    }
}
