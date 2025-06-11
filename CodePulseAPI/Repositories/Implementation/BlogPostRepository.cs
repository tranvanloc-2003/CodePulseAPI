using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }
        //lay all danh sach
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x =>x.Category).ToListAsync();
        }

        public async Task<BlogPost?> GetById(Guid id)
        {
           return await dbContext.BlogPosts.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }


        //chinh sua blogpost theo id
        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlogPost != null)
            {
                dbContext.Entry(blogPost).CurrentValues.SetValues(blogPost);
                await dbContext.SaveChangesAsync();
                return blogPost;
            }
            return null;
            
        }
    }
}
