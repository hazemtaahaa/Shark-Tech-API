
using Shark_Tech.DAL;
using System.Runtime.InteropServices;

namespace Shark_Tech.BL;

public class ProductManager: IProductManager
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IImageManager imageManager;


    public ProductManager(IUnitOfWork unitOfWork, IImageManager imageManager)
    {
        this.unitOfWork = unitOfWork;
        this.imageManager = imageManager;
    }

    //get all products
    public async Task<IReadOnlyList<ProductDTO>> GetAllProducts()
    {
        var products = await unitOfWork.ProductRepository.GetAllWithCategoryAndImagesAsync();

       

        // Include related entities
        var result = products.Select(x => new ProductDTO
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            NewPrice = x.NewPrice,
            OldPrice = x.OldPrice,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
            Quantity = x.Quantity,
            CategoryName = x.Category.Name,
            ProductImages = x.ProductImages.Select(y => new ProductImageDTO
            {
                ImageUrl = y.ImageUrl,
                ProductId = y.ProductId
            }).ToList()
        }).ToList();

        return result;
    }

    //get all products with sorting and filtering
    public async Task<IReadOnlyList<ProductDTO>> GetAllProducts(ProductParams productParams)
    {
        var products = await unitOfWork.ProductRepository.GetAllWithCategoryAndImagesAsync();

        //filter by search
        if(!string.IsNullOrEmpty(productParams.Search))
            products = products
                .Where(x => x.Name.ToLower().Contains(productParams.Search.ToLower()) 
                || x.Description.ToLower().Contains(productParams.Search.ToLower()));
        //filter by category
        if (productParams.CategoryId.HasValue)
            products = products.Where(x => x.CategoryId == productParams.CategoryId.Value);

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            products = productParams.Sort switch
            {
                "PriceAce" => products.OrderBy(x => x.NewPrice),
                "PriceDce" => products.OrderByDescending(x => x.NewPrice),
                _ => products.OrderBy(x => x.Name),
            };
        }
       
        //pagination
        if (productParams.PageSize>0 && productParams.PageNumber.HasValue)
        {
            products = products.Skip((productParams.PageNumber.Value - 1) * productParams.PageSize).Take(productParams.PageSize);
        }

        // Include related entities
        var result = products.Select(x => new ProductDTO
       {
           Id = x.Id,
           Name = x.Name,
           Description = x.Description,
           NewPrice = x.NewPrice,
           OldPrice = x.OldPrice,
           CreatedAt = x.CreatedAt,
           UpdatedAt = x.UpdatedAt,
           Quantity = x.Quantity,
           CategoryName = x.Category.Name,
           ProductImages = x.ProductImages.Select(y => new ProductImageDTO
           {
               ImageUrl = y.ImageUrl,
               ProductId = y.ProductId
           }).ToList()
       }).ToList();

        return result;
    }

    //add product
    public async Task<bool> AddProduct(AddProductDTO productDTO)
    {
        if (productDTO is null)
        {
            return false;
        }
        // Ensure the directory for images exists
        string imagesDirectory = Path.Combine("wwwroot", "Images", productDTO.Name);
        if (!Directory.Exists(imagesDirectory))
        {
            Directory.CreateDirectory(imagesDirectory);
        }
        // Add product images
        var productImages = await imageManager.AddImageAsync(productDTO.ProductImages, productDTO.Name);

        List<ProductImage>? productImageList = productImages.Select(
           x => new ProductImage
           {
               Id = Guid.NewGuid(),
               ImageUrl = x,
           }).ToList();

        var product = new Product
        {
            Name = productDTO.Name,
            Description = productDTO.Description,
            NewPrice = productDTO.NewPrice,
            OldPrice = productDTO.OldPrice,
            CategoryId = productDTO.CategoryId,
            Quantity = productDTO.Quantity,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            ProductImages = productImageList
        };
       


        await unitOfWork.ProductRepository.AddAsync(product);
        
       

        

        //foreach (var image in productImageList)
        //{
        //    await unitOfWork.ProductImageRepository.AddAsync(image);
        //}

        await unitOfWork.CompleteAsync();
        return true;
    }


    //update product
    public async Task<bool> UpdateProduct(UpdateProductDTO productDTO)
    {

        //find the product
        var product = await unitOfWork.ProductRepository.GetByIdWithCategoryAndImagesAsync(productDTO.Id);
        if (product is null)
        {
            return false;
        }

        Console.WriteLine("---->",product.Category.Name);

        product.Id = productDTO.Id;
        product.Name = productDTO.Name;
        product.Description = productDTO.Description;
        product.NewPrice = productDTO.NewPrice;
        product.OldPrice = productDTO.OldPrice;
        product.CategoryId = productDTO.CategoryId;
        product.Quantity = productDTO.Quantity;
        product.UpdatedAt = DateTime.Now;

        // Update the product in the database
        await unitOfWork.ProductRepository.UpdateAsync(product);

        //remove old images
        var oldImages = await unitOfWork.ProductImageRepository.GetAllAsync(x => x.Product);
        foreach (var image in oldImages)
        {
            await imageManager.DeleteImageAsync(image.ImageUrl);
            await unitOfWork.ProductImageRepository.DeleteAsync(image.Id);
        }


        // Add new product images
        var productImages = await imageManager.AddImageAsync(productDTO.ProductImages, productDTO.Name);

        var productImageList = productImages.Select(
            x => new ProductImage
            {
                Id = Guid.NewGuid(),
                ImageUrl = x,
                ProductId = product.Id,
            }).ToList();

        foreach (var image in productImageList)
        {           
            await unitOfWork.ProductImageRepository.AddAsync(image);        
        }

        await unitOfWork.CompleteAsync();
        return true;

    }

    // delete product
    public async Task<bool> DeleteProduct(Guid id)
    {
        //find the product
        var product = await unitOfWork.ProductRepository.GetByIdWithCategoryAndImagesAsync(id);

        if (product is null)
        {
            return false;
        }
        //remove old images
        var oldImages = await unitOfWork.ProductImageRepository.GetAllAsync(x => x.Product);
        foreach (var image in oldImages)
        {
            await imageManager.DeleteImageAsync(image.ImageUrl);
            await unitOfWork.ProductImageRepository.DeleteAsync(image.Id);
        }
        //remove the product
        await unitOfWork.ProductRepository.DeleteAsync(id);
        await unitOfWork.CompleteAsync();
        return true;


    }

}
