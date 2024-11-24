using Microsoft.AspNetCore.Http;

namespace Application.DTO.Request
{
    public record UploadProfilePhoto (string userId , IFormFile profilePhoto);
}
