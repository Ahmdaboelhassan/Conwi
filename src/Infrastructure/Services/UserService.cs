using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Domain.Entity;
using Infrastructure.Data;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.IServices;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly ApplicationDbContext _db;

        public UserService(UserManager<AppUser> userManger, IMapper mapper,
                 IPhotoService photoService,ApplicationDbContext db )
        {
            _userManger = userManger;
            _mapper = mapper;
            _photoService = photoService;
            _db = db;
        }

        public async Task<UserProfile?> GetUserProfileAsync(string userEmail)
        {
            var user = await _userManger.FindByEmailAsync(userEmail);

            if(user is null)
                return null;

            return _mapper.Map<UserProfile>(user);
        }

        public async Task<bool> UploadProfilePhotoAsync(string email, IFormFile photo){

            var user = await _userManger.FindByEmailAsync(email);

            if(user is null)  return false;
            
            var result = await _photoService.UploadProfilePhotoAsync(user.UserName, photo);

            if (result.Error is not null)  return false;
            
            user.PhotoURL = result.SecureUrl.AbsoluteUri;
            await _userManger.UpdateAsync(user);
            return true;
        }

        public async Task<bool> AddPostAsync(PostModel model)
        {
           if(model.content.Length == 0) return false;

           var user = await _userManger.FindByEmailAsync(model.email);

           if(user is null) return false;

           var result = await _photoService.UploadPostPhotoAsync(user.UserName, model.photo);

           if(result.Error is not null) return false;

           var newPost = new Post {
                content = model.content,
                timePosted = DateTime.UtcNow,
                UserPostedId = user.Id,
                photoURL = result.SecureUrl.AbsoluteUri
           };

            _db.Posts.Add(newPost);
            if(_db.SaveChanges() == 0) return false;
            return true; 
        }
        
    }
}