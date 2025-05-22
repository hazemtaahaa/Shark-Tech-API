using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shark_Tech.BL;
using Shark_Tech.DAL;

namespace Shark_Tech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {

        private readonly IProductManager productManager;
        private readonly IImageManager imageManager;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IProductManager productManager) : base(unitOfWork, mapper)
        {
            this.productManager = productManager;
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] ProductParams productParams)
        {
            try
            {
                var products = await productManager.GetAllProducts(productParams);

                if (products is null||products.Count==0)
                {
                    return BadRequest(new GeneralResult(400, "No Products Found!"));
                }
                var totalCount = await unitOfWork.ProductRepository.CountAsync();

                var result = new Pagination<ProductDTO>(productParams.PageNumber, productParams.PageSize, totalCount, products);

                return Ok(new GeneralResult<Pagination<ProductDTO>>(200, result, "Products retrieved successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.GetByIdAsync(id, x => x.Category, x => x.ProductImages);
                var result = mapper.Map<ProductDTO>(product);
                if (product is null)
                {
                    return BadRequest(new GeneralResult(400, "No Product Found!"));
                }
                return Ok(new GeneralResult<ProductDTO>(200, result, "Product get Succsfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct( AddProductDTO productDTO)
        {

            try
            {
                
                var result = await productManager.AddProduct(productDTO);
                if (result)
                {
                    return Ok(new GeneralResult(200, "Product Added Succsfully"));
                }
                return BadRequest(new GeneralResult(400, "Product Not Added!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct( UpdateProductDTO productDTO)
        {
            try
            {
                await productManager.UpdateProduct(productDTO);
                return Ok(new GeneralResult(200, "Product Updated Succsfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await productManager.DeleteProduct(id);
                return Ok(new GeneralResult(200, "Product Deleted Succsfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
