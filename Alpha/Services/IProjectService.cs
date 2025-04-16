using Alpha.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Services
{
    public interface IProjectService
    {
        Task<List<ProjectModel>> GetAllAsync();
        Task<List<ProjectModel>> GetByStatusAsync(ProjectStatus status);
        Task<ProjectModel> GetByIdAsync(string id);
        Task AddAsync(ProjectModel project);
        Task UpdateAsync(ProjectModel project);
        Task DeleteAsync(string id);

    }
}
