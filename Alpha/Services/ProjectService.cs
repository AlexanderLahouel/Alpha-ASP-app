using Microsoft.EntityFrameworkCore;
using Alpha.Data;
using Alpha.Models;


namespace Alpha.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        public ProjectService(AppDbContext context) => _context = context;

        public async Task<List<ProjectModel>> GetAllAsync() =>
            await _context.Projects.ToListAsync();

        public async Task<List<ProjectModel>> GetByStatusAsync(ProjectStatus status) =>
            await _context.Projects.Where(p => p.Status == status).ToListAsync();

        public async Task<ProjectModel?> GetByIdAsync(string id) =>
            await _context.Projects.FindAsync(id);


        public async Task AddAsync(ProjectModel project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectModel project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

    }
}