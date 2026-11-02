using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;

namespace PortfolioApi.Projects;

public class ProjectService
{
    private readonly PortfolioDbContext db;

    public ProjectService(PortfolioDbContext db)
    {
        this.db = db;
    }

    public async Task<List<ProjectResponse>> GetAllAsync()
    {
        return await db.Projects
            .AsNoTracking()
            .Select(project => new ProjectResponse(
                project.Id,
                project.Titel,
                project.Beschrijving,
                project.Categorie,
                project.GitHubUrl,
                project.Datum))
            .ToListAsync();
    }

    public async Task<ProjectResponse?> GetByIdAsync(int id)
    {
        return await db.Projects
            .AsNoTracking()
            .Where(project => project.Id == id)
            .Select(project => new ProjectResponse(
                project.Id,
                project.Titel,
                project.Beschrijving,
                project.Categorie,
                project.GitHubUrl,
                project.Datum))
            .FirstOrDefaultAsync();
    }

    public async Task<ProjectResponse> CreateAsync(ProjectInput input)
    {
        var project = new Project
        {
            Titel = input.Titel,
            Beschrijving = input.Beschrijving,
            Categorie = input.Categorie,
            GitHubUrl = input.GitHubUrl,
            Datum = input.Datum
        };

        db.Projects.Add(project);
        await db.SaveChangesAsync();

        return ToResponse(project);
    }

    public async Task<ProjectResponse?> UpdateAsync(int id, ProjectInput input)
    {
        var project = await db.Projects.FindAsync(id);

        if (project is null)
        {
            return null;
        }

        project.Titel = input.Titel;
        project.Beschrijving = input.Beschrijving;
        project.Categorie = input.Categorie;
        project.GitHubUrl = input.GitHubUrl;
        project.Datum = input.Datum;

        await db.SaveChangesAsync();

        return ToResponse(project);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await db.Projects.FindAsync(id);

        if (project is null)
        {
            return false;
        }

        db.Projects.Remove(project);
        await db.SaveChangesAsync();

        return true;
    }

    private static ProjectResponse ToResponse(Project project)
    {
        return new ProjectResponse(
            project.Id,
            project.Titel,
            project.Beschrijving,
            project.Categorie,
            project.GitHubUrl,
            project.Datum);
    }
}