
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class PostRepo(ApplicationDBContext context) : IPostRepo
{
    private readonly ApplicationDBContext _context = context;
    public async Task<Post> Add(Post Post)
    {
        _context.Posts.Add(Post);
        await _context.SaveChangesAsync();
        return Post;
    }

    public async Task<string> Delete(int id)
    {
        var a = await GetById(id);
        _context.Posts.Remove(a);
        await _context.SaveChangesAsync();
        return "Deleted";
    }

    public async Task<List<Post>> GetAll()
    {
        var b = await _context.Posts.Include(x => x.Tags).ToListAsync();
        return b;
    }

    public async Task<Post> GetById(int id)
    {
        var product = await _context.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.Id==id);
        return product;
    }

    public async Task Update(Post Post)
    {
        var a = await _context.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == Post.Id);

        if(a != null)
        {
            a.Title = Post.Title;
            a.Text = Post.Text;
            a.Tags = Post.Tags;
        }

        await _context.SaveChangesAsync();
    }
}
