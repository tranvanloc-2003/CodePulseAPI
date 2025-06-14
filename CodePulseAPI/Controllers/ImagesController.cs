using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO.BlogPost;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        //GET: {apibaseurl}/api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImage()
        {
            //goi repository de lay hinh anh
           var images =  await imageRepository.GetAsync();
            //chuyen doi domain sang dto
            var response = new List<BlogImageDto>();
            foreach(var blogimage in images)
            {
                response.Add(new BlogImageDto
                {
                    Id = blogimage.Id,
                    Title = blogimage.Title,
                    FileExtension = blogimage.FileExtension,
                    Url = blogimage.Url,
                    FileName = blogimage.FileName,
                    DateCreated = blogimage.DateCreated,
                });
            }
            return Ok(response);
        }
        // POST: {apibaseurl}/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            ValidateFileUpload(request.File);

            if (ModelState.IsValid)
            {
                // File upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(request.File.FileName).ToLower(),
                    FileName = request.FileName,
                    Title = request.Title,
                    DateCreated = DateTime.Now
                };

                blogImage = await imageRepository.Upload(request.File, blogImage);

                // Convert Domain Model to DTO
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }
    }
}