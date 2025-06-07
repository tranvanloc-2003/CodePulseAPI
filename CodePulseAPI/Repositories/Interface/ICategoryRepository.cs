using CodePulseAPI.Models.Domain;

namespace CodePulseAPI.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Categories> CreateAsync(Categories categories);
        Task<IEnumerable<Categories>> GetAllAsync();
    }
}
