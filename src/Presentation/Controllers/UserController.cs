using Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserProfile(string email)
        {

            var userProfile = await _userService.GetUserProfileAsync(email);
            if (userProfile is null)
                return BadRequest("Invalid Email");

            return Ok(userProfile);
        }


        [HttpPost("ProfilePhoto/{email}")]
        public async Task<ActionResult<string>> ProfilePhoto(string email, [FromForm] IFormFile photo)
        {

            if (!ValidatePhoto(photo))
                return BadRequest("Photo Must be png or jbg in size  1MB");

            var result = await _userService.UploadProfilePhotoAsync(email, photo);

            if (!result)
                return BadRequest("Somthing went wrong");

            return Ok("Image Uploaded Successfully");
        }

        private bool ValidatePhoto(IFormFile photo)
        {
            const int _MaxSize = 1048576;

            string[] _AllowedExtension = { ".jbeg", ".jbg", ".png" };

            if (photo.Length > _MaxSize) return false;

            if (!_AllowedExtension.Contains(Path.GetExtension(photo.FileName))) return false;

            return true;

        }
    }
}