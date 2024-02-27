using API.Models;

namespace API.Services.IServices
{
    public interface IUserService
    {
        Task<UserProfile?> GetUserProfileAsync(string userEmail); 
        Task<bool> UploadProfilePhotoAsync(string email, IFormFile photo);
        Task<bool> AddPostAsync(PostModel model);
      
    }
}