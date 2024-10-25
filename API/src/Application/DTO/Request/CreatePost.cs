using Microsoft.AspNetCore.Http;

namespace Application.DTO.Request;

public record class CreatePost(string userId , string content, IFormFile? photo){ }