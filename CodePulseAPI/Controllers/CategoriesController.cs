using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
    //https:localhost:xxxx/api/categoties
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        //post
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoriesRequestDto request)
        {
            var category = new Categories
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            await categoryRepository.CreateAsync(category);

            var response = new CategoriesDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = request.UrlHandle,
            };

            return Ok(response);
        }
        //Get: https://localhost:7153/api/Categories
        //lay danh sachs APi category
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();
            var response = new List<CategoriesDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoriesDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });

            }
            return Ok(response);
        }
        //GET: https://localhost:7153/api/Categories/{id}
        //https://localhost:7153/api/Categories/1
        //lay duy nhat 1 id de cos the chinh sua categories
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
           var existingCategory = await categoryRepository.GetById(id);
            if (existingCategory is null)
            {
                return NotFound();
            }

            var response = new CategoriesDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle,
            };
            return Ok(response);
        }
    }
}
