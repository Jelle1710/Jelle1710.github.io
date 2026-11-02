using Microsoft.AspNetCore.Http.HttpResults;

namespace PortfolioApi.Blogposts;

public static class BlogpostEndpoints
{
    public static IEndpointRouteBuilder MapBlogpostEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/blogposts")
            .WithTags("Blogposts");

        group.MapGet("/", GetAll)
            .WithName("GetBlogposts");

        group.MapGet("/{id:int}", GetById)
            .WithName("GetBlogpost");

        group.MapPost("/", Create)
            .WithName("CreateBlogpost");

        group.MapPut("/{id:int}", Update)
            .WithName("UpdateBlogpost");

        group.MapDelete("/{id:int}", Delete)
            .WithName("DeleteBlogpost");

        return app;
    }

    private static async Task<Ok<List<BlogpostResponse>>> GetAll(
        BlogpostService service)
    {
        var blogposts = await service.GetAllAsync();
        return TypedResults.Ok(blogposts);
    }

    private static async Task<Results<Ok<BlogpostResponse>, NotFound>> GetById(
        int id,
        BlogpostService service)
    {
        var blogpost = await service.GetByIdAsync(id);

        return blogpost is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(blogpost);
    }

    private static async Task<CreatedAtRoute<BlogpostResponse>> Create(
        BlogpostInput input,
        BlogpostService service)
    {
        var blogpost = await service.CreateAsync(input);

        return TypedResults.CreatedAtRoute(
            blogpost,
            "GetBlogpost",
            new { id = blogpost.Id });
    }

    private static async Task<Results<Ok<BlogpostResponse>, NotFound>> Update(
        int id,
        BlogpostInput input,
        BlogpostService service)
    {
        var blogpost = await service.UpdateAsync(id, input);

        return blogpost is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(blogpost);
    }

    private static async Task<Results<NoContent, NotFound>> Delete(
        int id,
        BlogpostService service)
    {
        var deleted = await service.DeleteAsync(id);

        return deleted
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}