using CloudinaryDotNet.Actions;

namespace API.Services.IServices
{
    public interface IPhotoService 
    {
        Task<ImageUploadResult> UploadProfilePhotoAsync(string UserName , IFormFile photo);
        Task<ImageUploadResult> UploadPostPhotoAsync(string UserName , IFormFile photo);
    }
}