using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_3.Controllers;

public class TagsController(ITagRepo TagRepo) : Controller
{
    private readonly ITagRepo _repo = TagRepo;

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
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tag Tag)
    {
        if (!ModelState.IsValid)
        return View(Tag);
        await _repo.Add(Tag);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var Tag = await _repo.GetById(id);
        return View(Tag);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Tag Tag)
    {
        await _repo.Update(Tag);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
