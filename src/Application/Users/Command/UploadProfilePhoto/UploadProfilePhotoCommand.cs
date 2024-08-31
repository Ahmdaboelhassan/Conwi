using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Command.UploadProfilePhoto;

public record UploadProfilePhotoCommand(string email, IFormFile photo) : IRequest<bool>;
