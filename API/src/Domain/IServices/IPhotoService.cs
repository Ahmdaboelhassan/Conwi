using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Domain.IServices
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadProfilePhotoAsync(string UserName, IFormFile photo);
        Task<ImageUploadResult> UploadPostPhotoAsync(string UserName, IFormFile photo);
    }
}