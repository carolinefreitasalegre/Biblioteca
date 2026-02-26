using CloudinaryDotNet.Actions; // O ImageUploadResult precisa disso!
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Services.Contracts;

public interface IUploadPhotoService
{
    Task<ImageUploadResult> UploadImageAsync(IFormFile file);
}