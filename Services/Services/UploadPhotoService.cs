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
    
    
    public async Task<ImageUploadResult> UploadImageAsync(string bas64Image)
    {
       if(string.IsNullOrWhiteSpace(bas64Image))
           return null;
        
        var base64Data = bas64Image.Contains(',')
            ? bas64Image.Split(',')[1]
            : bas64Image;

        byte[] imageBytes;
        try
        {
            imageBytes = Convert.FromBase64String(base64Data);
        }
        catch(FormatException ex)
        {
            throw new ArgumentException($"String base64 inválida. Tamanho recebido: {base64Data.Length} caracteres. " +
            $"Verifique se o conteúdo enviado é realmente a imagem codificada em Base64, e não o nome do arquivo.",
            ex);
        }

        using var stream = new MemoryStream(imageBytes);
        
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription("capa.jpg", stream),
            Folder = "biblioteca/capas",
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