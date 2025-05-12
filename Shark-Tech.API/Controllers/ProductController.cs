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
        public async Task<IActionResult> GetAll(string? sort,Guid? CategoryId,int? PageSize,int? PageNumber)
        {
            try
            {
                //  var products = await unitOfWork.ProductRepository.GetAllAsync(x=>x.Category,x=>x.ProductImages);
               

                var products = await productManager.GetAllProducts(sort,CategoryId,PageSize,PageNumber);
                var result = mapper.Map<IReadOnlyList<ProductDTO>>(products);
                if (products is null)
                {
                    return BadRequest(new GeneralResult(400,"No Prodcuts Found!"));
                }
                return Ok(new GeneralResult<IReadOnlyList<ProductDTO>>(200,result,"Products get Succsfully"));
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
