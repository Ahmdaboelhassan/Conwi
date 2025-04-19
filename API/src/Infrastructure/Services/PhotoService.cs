using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Application.Helper;
using Microsoft.AspNetCore.Http;
using Domain.IServices;

namespace Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly CloudinaryAccount _cloudinary;
        private readonly Account _account;
        public PhotoService(IOptions<CloudinaryAccount> cloudinary)
        {
            _cloudinary = cloudinary.Value;
            _account = new Account(_cloudinary.CloudName,
                                _cloudinary.ApiKey,_cloudinary.ApiSecert);
        }

        public async Task<ImageUploadResult> UploadProfilePhotoAsync(string UserName, IFormFile photo)
        {  
           var cloudinary = new Cloudinary(_account);

           using var stream = photo.OpenReadStream();

           var uploadParams = new ImageUploadParams(){
                Folder = "Conwi/" + UserName ,
                File = new FileDescription(photo.FileName , stream),
                Transformation = new Transformation()
                            .Height(500).Width(500).Gravity("face").Crop("fill")            
           };

           return await cloudinary.UploadAsync(uploadParams);
        }

        public async Task<ImageUploadResult> UploadPostPhotoAsync(string UserName , IFormFile photo)
        {
            var cloudinary = new Cloudinary(_account);

            using var stream = photo.OpenReadStream();
        
             var uploadParams = new ImageUploadParams(){
                Folder = "Conwi/"+ UserName,
                File = new FileDescription(photo.FileName , stream),           
           };

           return await cloudinary.UploadAsync(uploadParams);
        }
    }
}