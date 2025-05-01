using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shark_Tech.API.Controllers;
using Shark_Tech.BL;
using Shark_Tech.DAL;

namespace Shark_Tech.API;


public class CategoriesController : BaseController
{
    public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    [HttpGet ("get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categories = await unitOfWork.CategoryRepository.GetAllAsync();
            if(categories is null)
            {
                return BadRequest();
            }
            return Ok(categories);
        }
        catch (Exception ex)
        {

           return BadRequest(ex.Message);
        }
    }

    [HttpGet ("get-by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return BadRequest();
            }
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add-Category")]

    public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryDTO.Name,
                Description = categoryDTO.Description
            };
            await unitOfWork.CategoryRepository.AddAsync(category);
           
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update-Category/{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return BadRequest();
            }
            category.Name = categoryDTO.Name;
            category.Description = categoryDTO.Description;
            await unitOfWork.CategoryRepository.UpdateAsync(category);

            return Ok(new {message ="Item has been Updated"});

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete-Category/{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        try
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return BadRequest();
            }
            await unitOfWork.CategoryRepository.DeleteAsync(id);
            return Ok(new { message = "Item has been Deleted" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
