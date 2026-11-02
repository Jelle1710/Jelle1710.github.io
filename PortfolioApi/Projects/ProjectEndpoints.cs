using Microsoft.AspNetCore.Http.HttpResults;

namespace PortfolioApi.Projects;

public static class ProjectEndpoints
{
    public static IEndpointRouteBuilder MapProjectEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/projects")
            .WithTags("Projects");

        group.MapGet("/", GetAll)
            .WithName("GetProjects");

        group.MapGet("/{id:int}", GetById)
            .WithName("GetProject");

        group.MapPost("/", Create)
            .WithName("CreateProject");

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateProject");

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteProject");

        return app;
    }

    private static async Task<Ok<List<ProjectResponse>>> GetAll(
        ProjectService service)
    {
        var projects = await service.GetAllAsync();
        return TypedResults.Ok(projects);
    }

    private static async Task<Results<Ok<ProjectResponse>, NotFound>> GetById(
        int id,
        ProjectService service)
    {
        var project = await service.GetByIdAsync(id);

        return project is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(project);
    }

    private static async Task<CreatedAtRoute<ProjectResponse>> Create(
        ProjectInput input,
        ProjectService service)
    {
        var project = await service.CreateAsync(input);

        return TypedResults.CreatedAtRoute(
            project,
            "GetProject",
            new { id = project.Id });
    }

    private static async Task<Results<Ok<ProjectResponse>, NotFound>> Update(
        int id,
        ProjectInput input,
        ProjectService service)
    {
        var project = await service.UpdateAsync(id, input);

        return project is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(project);
    }

    private static async Task<Results<NoContent, NotFound>> Delete(
        int id,
        ProjectService service)
    {
        var deleted = await service.DeleteAsync(id);

        return deleted
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}