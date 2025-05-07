using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Shark_Tech.BL;

public static class BusinessExtensions
{
    public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IImageManager, ImageManager>();
        services.AddScoped<IProductManager, ProductManager>();
        services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));
    }
}
