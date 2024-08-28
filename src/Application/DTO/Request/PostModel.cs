using Microsoft.AspNetCore.Http;

namespace Application.DTO.Request
{
    public record class PostModel(string email, string UserName,
            string content, IFormFile photo)
    { }

}