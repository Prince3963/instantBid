using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public interface ICloudinaryService
{
    Task<string> uploadImages(IFormFile file);
}

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary cloudinary;
    public CloudinaryService(IConfiguration configuration)
    {
        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];

        var account = new Account(cloudName, apiKey, apiSecret);
        cloudinary = new Cloudinary(account);

        if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
        {
            Console.WriteLine("ERROR: Cloudinary credentials are missing or invalid.");
        }
    }



    public async Task<string> uploadImages(IFormFile file)
    {
        Console.WriteLine("file received in CloudinaryService: " + file?.FileName);
        Console.WriteLine("UploadImages() called");

        if (file == null)
        {
            Console.WriteLine("ERROR: No file was received.");
            return null;
        }

        if (file.Length == 0)
        {
            Console.WriteLine("ERROR: Uploaded file is empty.");
            return null;
        }

        if (file.Length > 10 * 1024 * 1024)  // Max size 10MB
        {
            Console.WriteLine("ERROR: File is too large.");
            return null;
        }
        try
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    //Transformation = new Transformation().Width(250).Height(250).Crop("fill")
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK || string.IsNullOrEmpty(uploadResult.SecureUrl?.ToString()))
                {
                    Console.WriteLine("ERROR: Cloudinary upload failed. Status: " + uploadResult.StatusCode);
                    Console.WriteLine("Error message: " + uploadResult.Error?.Message);
                    return null;
                }

                return uploadResult.SecureUrl.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in uploadImages: " + ex.Message);
            return "Error: Exception occurred during upload - " + ex.Message;
        }
    }

}