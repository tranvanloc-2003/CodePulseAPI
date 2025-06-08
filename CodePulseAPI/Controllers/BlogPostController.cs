using CodePulseAPI.Models.Domain;
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

        public BlogPostController( IBlogPostRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(BlogPostDto request)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                UrlHandle = request.UrlHandle,
                FeaturedImageUrl = request.FeaturedImageUrl,
                DateCreate = request.DateCreate,
                Author  = request.Author,
                Invisible = request.Invisible,
            };
            await repository.CreateAsync(blogPost);
            var response = new BlogPostDto
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                UrlHandle = request.UrlHandle,
                FeaturedImageUrl = request.FeaturedImageUrl,
                DateCreate = request.DateCreate,
                Author = request.Author,
                Invisible = request.Invisible,

            };
            return Ok(response);
        }
    }
}
