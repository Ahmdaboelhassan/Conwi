namespace API.Models
{
    public record class PostModel (string email, string UserName,
            string content, IFormFile photo){}

}