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

        public async Task<Categories> GetById(Guid id)
        {
            return await dbContext.categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Categories> UpdateAsync(Categories categories)
        {
          var existingCategory =  await dbContext.categories.FirstOrDefaultAsync(x => x.Id == categories.Id);
            if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(categories);
                await dbContext.SaveChangesAsync();
                return categories;
            }
            return null;
        } 
        public async Task<Categories?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory is null)
            {
                return null;
            }
            dbContext.categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
