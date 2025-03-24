using Community.Repository.Entities;
using Community.Repository.Interfaces;
using Community.Repository.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Community.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryRepo;

        public CategoryController(ICategory categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Invalid category data.");

            var createdCategory = _categoryRepo.CreateCategory(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, createdCategory);
        }


        [HttpGet("GetCategoryById/{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepo.GetCategoryById(id);
            if (category == null)
                return NotFound($"No category found with ID {id}.");
            return Ok(category);
        }

        [HttpGet("GetAllCategories")]
        public  IActionResult GetAllCategories()
        {
            var categories =  _categoryRepo.GetAllCategories();
            return Ok(categories);
        }

        
    }
}


