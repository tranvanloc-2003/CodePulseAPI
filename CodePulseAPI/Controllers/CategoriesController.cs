using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Models.DTO.Categories;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> CreateCategory(CreateCategoriesRequestDto request)
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
        [Authorize (Roles = "Writer")]// uy quyen
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
        //GET: https://localhost:7153/api/Categories/{id}Add commentMore actions
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
        //PUT: https://localhost:7153/api/Categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            var category = new Categories
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            category = await categoryRepository.UpdateAsync(category);
            if (category is null)
            {
                return NotFound();
            }
            var response = new CategoriesDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }
        //DELETE: https://localhost:7153/api/Categories/{id}
        //Xoa san pham thong qua id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
           var category =  await categoryRepository.DeleteAsync(id);
            if(category is null)
            {
                return NotFound();
            }
            var response = new CategoriesDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,

            };
            return Ok(response);
        }
    }
}
