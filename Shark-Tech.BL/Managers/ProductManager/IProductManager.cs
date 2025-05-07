namespace Shark_Tech.BL;

public interface IProductManager
{
    Task<bool> AddProduct(AddProductDTO productDTO);
    Task<bool> UpdateProduct(UpdateProductDTO productDTO);

    Task<bool> DeleteProduct(Guid id);
}
