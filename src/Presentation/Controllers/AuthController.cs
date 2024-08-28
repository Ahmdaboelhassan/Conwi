using Application.Helper;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _emailService = emailService;
            _authService = authService;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponse>> Register(Register model)
        {

            var AuthResponse = await _authService.RegisterAsync(model);

            if (!AuthResponse.IsAuthenticated)
                return BadRequest(AuthResponse.Messages);


            return Ok(AuthResponse);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login(LogIn model)
        {

            var AuthResponse = await _authService.LoginAsync(model);

            if (!AuthResponse.IsAuthenticated)
                return BadRequest(AuthResponse.Messages);

            return Ok(AuthResponse);
        }

        [HttpGet("GetEmailConfirmationUrl")]
        public async Task<ActionResult<string>> GetEmailConfirmationUrl(ConfirmationRequest model)
        {

            var confirmationResponse = await _authService.GetEmailConfirmationToken(model.Email);

            if (!confirmationResponse.isGenerated)
                return BadRequest(confirmationResponse.Messages);

            // send email by confirm link 
            await _emailService.SendEmail(model.Email, "Confirm Conwi Email", GetHtmlTemplate(model.ConfirmationUrl, ConfirmType.EmailConfrimation));

            var emailConfirmationUrl =
                GetConfirmationLink(HttpContext, model.Email, confirmationResponse.Token, ConfirmType.EmailConfrimation);

            return Ok(emailConfirmationUrl);
        }

        [HttpGet("GetPasswordResetUrl")]
        public async Task<ActionResult<string>> GetPasswordResetUrl(ConfirmationRequest model)
        {

            var confirmResponse = await _authService.GetPasswordResetToken(model.Email);

            if (!confirmResponse.isGenerated)
                return BadRequest(confirmResponse.Messages);

            await _emailService.SendEmail(model.Email, "Reset Your Password", GetHtmlTemplate(model.ConfirmationUrl, ConfirmType.PasswordReset));

            var passwordResetLink =
                     GetConfirmationLink(HttpContext, model.Email, confirmResponse.Token, ConfirmType.PasswordReset);

            return Ok(passwordResetLink);
        }

        [HttpPost("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string email, string token)
        {

            var confirmEmail = await _authService.ConfrimEmailAsync(email, token);

            if (!confirmEmail)
                return BadRequest("Email has not confirmed");

            return Ok("email confirmed successfully");
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword([FromQuery] string email, string token, string newPassword)
        {

            var isPasswordReset = await _authService.ResetPasswordAsync(email, token, newPassword);

            if (!isPasswordReset)
                return BadRequest("An Error Occured");

            return Ok("Password Has Been Reseted Successfully");

        }

        // private helper methods
        private string GetConfirmationLink(HttpContext context, string email,
                string token, ConfirmType type)
        {

            string isHttps = context.Request.IsHttps ? "https://" : "Http://";

            string currentDomain = context.Request.Host.Value;

            var actionUrl = Url.Action(nameof(ConfirmEmail), "Auth", new { email, token });

            if (type == ConfirmType.PasswordReset)
                actionUrl = Url.Action(nameof(ResetPassword), "Auth", new { email, token });

            return $"{isHttps}{currentDomain}{actionUrl}";
        }

        private string GetHtmlTemplate(string confirmUrl, ConfirmType type)
        {
            if (type == ConfirmType.PasswordReset)
                return $"<a href=\"{confirmUrl}\" >Click To Reset Your Password</a>";

            return $"<a href=\"{confirmUrl}\" >Click To Confirm Your Email</a>";
        }

    }
}