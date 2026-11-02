using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;

namespace PortfolioApi.Blogposts;

public class BlogpostService
{
    private readonly PortfolioDbContext db;

    public BlogpostService(PortfolioDbContext db)
    {
        this.db = db;
    }

    public async Task<List<BlogpostResponse>> GetAllAsync()
    {
        return await db.Blogposts
            .AsNoTracking()
            .Select(blogpost => new BlogpostResponse(
                blogpost.Id,
                blogpost.Titel,
                blogpost.Inhoud,
                blogpost.Publicatiedatum))
            .ToListAsync();
    }

    public async Task<BlogpostResponse?> GetByIdAsync(int id)
    {
        return await db.Blogposts
            .AsNoTracking()
            .Where(blogpost => blogpost.Id == id)
            .Select(blogpost => new BlogpostResponse(
                blogpost.Id,
                blogpost.Titel,
                blogpost.Inhoud,
                blogpost.Publicatiedatum))
            .FirstOrDefaultAsync();
    }

    public async Task<BlogpostResponse> CreateAsync(BlogpostInput input)
    {
        var blogpost = new Blogpost
        {
            Titel = input.Titel,
            Inhoud = input.Inhoud,
            Publicatiedatum = input.Publicatiedatum
        };

        db.Blogposts.Add(blogpost);
        await db.SaveChangesAsync();

        return ToResponse(blogpost);
    }

    public async Task<BlogpostResponse?> UpdateAsync(
        int id,
        BlogpostInput input)
    {
        var blogpost = await db.Blogposts.FindAsync(id);

        if (blogpost is null)
        {
            return null;
        }

        blogpost.Titel = input.Titel;
        blogpost.Inhoud = input.Inhoud;
        blogpost.Publicatiedatum = input.Publicatiedatum;

        await db.SaveChangesAsync();

        return ToResponse(blogpost);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var blogpost = await db.Blogposts.FindAsync(id);

        if (blogpost is null)
        {
            return false;
        }

        db.Blogposts.Remove(blogpost);
        await db.SaveChangesAsync();

        return true;
    }

    private static BlogpostResponse ToResponse(Blogpost blogpost)
    {
        return new BlogpostResponse(
            blogpost.Id,
            blogpost.Titel,
            blogpost.Inhoud,
            blogpost.Publicatiedatum);
    }
}