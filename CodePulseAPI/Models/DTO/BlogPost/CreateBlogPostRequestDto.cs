﻿namespace CodePulseAPI.Models.DTO.BlogPost
{
    public class CreateBlogPostRequestDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string UrlHandle { get; set; }
        public string FeaturedImageUrl { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.UtcNow;
        public string Author { get; set; }
        public bool Isvisible { get; set; }
        //them nhieu mang danh muc
        public Guid[] Categories { get; set; }
    }
}
