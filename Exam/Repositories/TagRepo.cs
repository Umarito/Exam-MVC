
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class TagRepo(ApplicationDBContext context) : ITagRepo
{
    private readonly ApplicationDBContext _context = context;
    public async Task<Tag> Add(Tag Tag)
    {
        Tag.CreatedAt = DateTime.UtcNow;
        _context.Tags.Add(Tag);
        await _context.SaveChangesAsync();
        return Tag;
    }

    public async Task<string> Delete(int id)
    {
        var a = await GetById(id);
        _context.Tags.Remove(a);
        await _context.SaveChangesAsync();
        return "Deleted";
    }

    public async Task<List<Tag>> GetAll()
    {
        var b = await _context.Tags.Include(x => x.Posts).ToListAsync();
        return b;
    }

    public async Task<Tag> GetById(int id)
    {
        var product = await _context.Tags.Include(x => x.Posts).FirstOrDefaultAsync(x=>x.Id==id);
        return product;
    }

    public async Task Update(Tag Tag)
    {
        var a = await GetById(Tag.Id);

        if(a != null)
        {
            a.Name = Tag.Name;
        }

        await _context.SaveChangesAsync();
    }
}
