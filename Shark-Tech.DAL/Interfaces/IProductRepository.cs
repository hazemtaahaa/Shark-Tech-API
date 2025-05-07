

namespace Shark_Tech.DAL;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IReadOnlyList<Product>> GetAllWithCategoryAndImagesAsync();
    Task<Product> GetByIdWithCategoryAndImagesAsync(Guid id);
}
