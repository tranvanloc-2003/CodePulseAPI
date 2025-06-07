using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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
        //lay danh sách danh mục
        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
            return await dbContext.categories.ToListAsync();
        }

        public async Task<Categories?> GetById(Guid id)
        {
          return await dbContext.categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
