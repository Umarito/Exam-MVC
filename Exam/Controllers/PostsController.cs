using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lesson_3.Controllers;

public class PostsController(IPostRepo PostRepo, ApplicationDBContext context) : Controller
{
    private readonly IPostRepo _repo = PostRepo;
    private readonly ApplicationDBContext _context = context;

    public async Task<ActionResult> Index()
    {
        return View(await _repo.GetAll());
    }

    public async Task<IActionResult> GetById(int id)
    {
        var a = await _repo.GetById(id);
        return View(a);
    }

    public IActionResult Create()
    {
        ViewBag.Tags = _context.Tags.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post Post, List<int> selectedTagIds)
    {
        selectedTagIds ??= [];

        if (!ModelState.IsValid)
        {
            ViewBag.Tags = _context.Tags.ToList();
            return View(Post);
        }

        if (selectedTagIds.Any())
        {
            Post.Tags = await _context.Tags.Where(x => selectedTagIds.Contains(x.Id)).ToListAsync();
        }

        await _repo.Add(Post);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var Post = await _repo.GetById(id);
        ViewBag.Tags = _context.Tags.ToList();
        ViewBag.SelectedTagIds = Post.Tags.Select(x => x.Id).ToList();
        return View(Post);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Post Post, List<int> selectedTagIds)
    {
        selectedTagIds ??= [];

        Post.Tags = await _context.Tags
            .Where(x => selectedTagIds.Contains(x.Id))
            .ToListAsync();

        await _repo.Update(Post);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
