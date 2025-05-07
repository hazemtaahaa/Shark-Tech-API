using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Shark_Tech.BL;

public class ImageManager : IImageManager
{
    private readonly IFileProvider fileProvider;
    public ImageManager(IFileProvider fileProvider)
    {
        this.fileProvider = fileProvider;
    }
    public Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
    {

       List<string> imageUrls = new List<string>();
        var ImageDirectory = Path.Combine("wwwroot","Images", src);

        if (!Directory.Exists(ImageDirectory))
        {
            Directory.CreateDirectory(ImageDirectory);
        }

        foreach (var file in files)
        {
            if (file.Length == 0)
                continue;

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var filePath = $"/Images/{src}/{fileName}";

            var fullPath = Path.Combine(ImageDirectory, fileName);
            // Save the file to the server
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
            imageUrls.Add(filePath);
        }
        return Task.FromResult(imageUrls);
    }

    public Task DeleteImageAsync(string src)
    {
       var info = fileProvider.GetFileInfo(src);
        if (info.Exists)
        {

            File.Delete(info.PhysicalPath);
        }
        return Task.CompletedTask;
    }
}
