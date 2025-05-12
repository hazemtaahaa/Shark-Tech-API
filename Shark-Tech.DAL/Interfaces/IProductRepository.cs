

namespace Shark_Tech.DAL;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IQueryable<Product>> GetAllWithCategoryAndImagesAsync();
    Task<Product> GetByIdWithCategoryAndImagesAsync(Guid id);
}
