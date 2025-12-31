using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts;

public interface IUploadPhotoService
{
    Task<ImageUploadResult> UploadImageAsync(IFormFile file);
}