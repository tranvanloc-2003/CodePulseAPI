using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Models.DTO.BlogPost;
using CodePulseAPI.Repositories.Implementation;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository repository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostController( IBlogPostRepository repository,ICategoryRepository categoryRepository)
        {
            this.repository = repository;
            this.categoryRepository = categoryRepository;
        }
        // POST: https://localhost:7153/api/BlogPost
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {//chuyen dto sang domain
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                UrlHandle = request.UrlHandle,
                FeaturedImageUrl = request.FeaturedImageUrl,
                DateCreate = request.DateCreate,
                Author  = request.Author,
                Isvisible = request.Isvisible,
                Category = new List<Categories>()
            };
            //lay theo id category
            foreach(var categoryGuid in request.Categories)
            {
                var existingCategories = await categoryRepository.GetById(categoryGuid);
                if (existingCategories != null)
                {
                    blogPost.Category.Add(existingCategories);
                }
            }
            await repository.CreateAsync(blogPost);
            //chuyen domain sang dto
            var response = new BlogPostDto
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                UrlHandle = request.UrlHandle,
                FeaturedImageUrl = request.FeaturedImageUrl,
                DateCreate = request.DateCreate,
                Author = request.Author,
                Isvisible = request.Isvisible,
                Categories = blogPost.Category.Select(x => new CategoriesDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };
            return Ok(response);
        }
        //GET: http.../api/BlogPost
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogPosts = await repository.GetAllAsync();
            var response = new List<BlogPostDto>();
            foreach(var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    UrlHandle = blogPost.UrlHandle,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    DateCreate = blogPost.DateCreate,
                    Author = blogPost.Author,
                    Isvisible = blogPost.Isvisible,
                    
                });
            }
            return Ok(response);
        }
    }
}
