namespace CodePulseAPI.Models.DTO.BlogPost
{
    public class UploadImageRequest
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
    }
}