using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.Models;
using Services.Contracts;

namespace Services.Services;

public class UploadPhotoService : IUploadPhotoService
{
    
    private readonly Cloudinary _cloudinary;

    public UploadPhotoService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );
        
        _cloudinary = new Cloudinary(acc);
    }
    
    
    public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "",
            Transformation = new Transformation()
                .Width(500)
                .Height(750)
                .Crop("fill")
                .Quality("auto")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);


        return uploadResult;
    }
}