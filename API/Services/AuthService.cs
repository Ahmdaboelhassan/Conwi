using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DTOs;
using API.Helper;
using API.Models;
using API.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JWT _Jwt;

        public AuthService(UserManager<AppUser> userManager , IOptions<JWT> jwt 
            , IMapper mapper)
        { 
            _userManager = userManager;
            _mapper = mapper;
            _Jwt = jwt.Value;
        }
        public async Task<AuthResponse> RegisterAsync(Register model)
        {   
            // [1] Check if is there user with the same email
             var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
             if(userWithSameEmail is not null) 
                 return new AuthResponse {Messages = new List<string>{"This Email Is Aready Exists !!"}};

            // [2] Check if is there user with the same username
            var userWithSameUsername = await _userManager.FindByNameAsync(model.Username);
             if(userWithSameUsername is not null) 
                return new AuthResponse {Messages = new List<string>{"This Username Is Aready Exists !!"}};

            // [3] register user and send token 
            var newUser = _mapper.Map<AppUser>(model);

            var results = await _userManager.CreateAsync(newUser, model.Password);

            if(!results.Succeeded){
               var errorMessages = new List<string>();
               foreach(var error in results.Errors){
                    errorMessages.Append(error.Description);
               }
               return new AuthResponse {Messages = errorMessages};
            }

            var tokenInfo = await CreateToken(newUser);
            var tokenExpiresOn = tokenInfo.ValidTo;
            var token = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

            // make email confimation code
            await _userManager.GenerateEmailConfirmationTokenAsync(newUser);            

            return new AuthResponse{

                Messages = new List<string>{"User has been registered successfully"},
                Email = newUser.Email,
                UserName = newUser.UserName,
                Token = token,
                IsAuthenticated = true,
                ExpireOn = tokenExpiresOn,
            };
        }
        public async Task<AuthResponse> LoginAsync(LogIn model)
        {   
            // [1] checking the email and password
            var userWithSameUsername = await _userManager.FindByNameAsync(model.Email);
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if(userWithSameEmail is null && userWithSameUsername is null )
                return new AuthResponse{Messages = new List<string>{"Invalid Email Or Username !!"}};
            
            var userFromDb = new AppUser();

            if(userWithSameEmail is null)
                userFromDb = userWithSameUsername;

            if(userWithSameUsername is null)
                userFromDb = userWithSameEmail;

            //[2] check for password
            var isPasswordTrue = await _userManager.CheckPasswordAsync(userFromDb , model.Password);
            if(!isPasswordTrue)
                return new AuthResponse {Messages = new List<string> {"Password is not correct"}};

            var tokenInfo = await CreateToken(userFromDb);
            var tokenExpiresOn = tokenInfo.ValidFrom;
            var token = new JwtSecurityTokenHandler().WriteToken(tokenInfo);
            
            // check if is email has not confrimed
            var isConfrimed = await _userManager.IsEmailConfirmedAsync(userFromDb);
            
            return new AuthResponse{
                Messages = new List<string> {"You are signing in successfully"},
                IsAuthenticated = true,
                ExpireOn = tokenExpiresOn,
                Token = token,
                Email = userFromDb.Email,
                UserName = userFromDb.UserName,
                IsEmailConfrimed = isConfrimed
            };

        }
         
         public async Task<ConfirmationResponse> GetEmailConfirmationToken(string email){

            var userFromDb = await _userManager.FindByEmailAsync(email);
            if(userFromDb is null)
                return new ConfirmationResponse {Messages = "User Not Found"};
            
           string? confirmationToken =  await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);
           if(confirmationToken is null)
                return new ConfirmationResponse {Messages = "An Error Occured"};

           return new ConfirmationResponse {
            isGenerated = true,
            Token = confirmationToken,
            Messages = "Email Comfirmation Token Has Been Generated Successfully",
           };
         }

        public async Task<bool> ConfrimEmailAsync(string email , string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if( user is null ) return false;

            var results = await _userManager.ConfirmEmailAsync(user, token);

            return results.Succeeded;
        }

        public async Task<ConfirmationResponse?> GetPasswordResetToken(string email)
        {
            var userFromDb = await _userManager.FindByEmailAsync(email);
            if(userFromDb is null)
                return new ConfirmationResponse {Messages = "User Not Found!!"};

            string? token = await _userManager.GeneratePasswordResetTokenAsync(userFromDb);

            if(token is null)
                return new ConfirmationResponse {Messages = "An Error Occured!!"};

            return new ConfirmationResponse{
                Messages = "Password Reset Token Has Been Genrated Successfully",
                isGenerated = true,
                Token = token
            };
        }
        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var userFromDb = await _userManager.FindByEmailAsync(email);
            if( userFromDb is null )  return false;

            var result = await _userManager.ResetPasswordAsync(userFromDb, token , newPassword);

            return result.Succeeded;
        }


        // helper Methods
         private async Task<JwtSecurityToken> CreateToken(AppUser user)
        {   
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            }
            .Union(userClaims);

            var securitKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Key));

            var signingCredentials  = new SigningCredentials(securitKey,SecurityAlgorithms.HmacSha256);

            var jwtSecurtyToken = new JwtSecurityToken(
                issuer : _Jwt.Issuer,
                audience: _Jwt.Audience,
                signingCredentials : signingCredentials,
                claims : claims,
                expires : DateTime.UtcNow.AddDays(_Jwt.DurationInDays)
            );
            
            return jwtSecurtyToken;
        }

        private Task<string> RefreshToken()
        {
           throw new NotImplementedException();
        }
        
    }
}