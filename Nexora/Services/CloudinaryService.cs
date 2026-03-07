using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Nexora.Services;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration config)
    {
        var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string?> UploadAsync(IFormFile file, string folder = "products")
    {
        if (file.Length == 0) return null;

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = $"nexora/{folder}",
            Transformation = new Transformation().Quality("auto").FetchFormat("auto")
        };

        var result = await _cloudinary.UploadAsync(uploadParams);
        return result.StatusCode == System.Net.HttpStatusCode.OK ? result.SecureUrl.ToString() : null;
    }

    public async Task DeleteAsync(string publicId)
    {
        await _cloudinary.DestroyAsync(new DeletionParams(publicId));
    }
}
