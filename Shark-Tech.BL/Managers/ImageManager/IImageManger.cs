namespace Shark_Tech.BL;
using Microsoft.AspNetCore.Http;

public interface IImageManager
{
    Task<List<string>> AddImageAsync(IFormFileCollection files, string src);
    Task DeleteImageAsync(string src);

}
