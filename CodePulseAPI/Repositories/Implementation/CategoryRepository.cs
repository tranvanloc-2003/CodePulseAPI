using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Repositories.Interface;

namespace CodePulseAPI.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<Categories> CreateAsync(Categories category)
        {
           await dbContext.categories.AddAsync(category);
           await dbContext.SaveChangesAsync();
            return category;
        }
    }
}
