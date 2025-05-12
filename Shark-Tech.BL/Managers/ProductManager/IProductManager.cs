namespace Shark_Tech.BL;

public interface IProductManager
{
    Task<bool> AddProduct(AddProductDTO productDTO);
    Task<bool> UpdateProduct(UpdateProductDTO productDTO);
    Task<IReadOnlyList<ProductDTO>> GetAllProducts(string? sort, Guid? CategoryId, int? PageSize, int? PageNumber);
    Task<IReadOnlyList<ProductDTO>> GetAllProducts();
    Task<bool> DeleteProduct(Guid id);
}
