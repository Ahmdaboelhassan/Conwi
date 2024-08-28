using Application.DTO.Request;
using Application.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<UserProfile?> GetUserProfileAsync(string userEmail);
        Task<bool> UploadProfilePhotoAsync(string email, IFormFile photo);
        Task<bool> AddPostAsync(PostModel model);

    }
}