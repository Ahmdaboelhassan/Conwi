using MediatR;

namespace Application.Users.Command;

public record UploadProfilePhotoCommand(DTO.Request.UploadProfilePhoto model) : IRequest<bool>;
