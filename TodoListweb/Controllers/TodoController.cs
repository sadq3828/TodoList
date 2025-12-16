using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListweb.Models.Data;
public class TodoController : Controller
{
    private readonly ApplicationDbContext _context;

    public TodoController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var todos = await _context.Todos.OrderByDescending(t => t.CreatedAt).ToListAsync();
        return View(todos);
    }
    [HttpPost]
    public async Task<IActionResult> Create(string title,string description)
    {
        if(string.IsNullOrWhiteSpace(title))
        return RedirectToAction(nameof(Index));

        var todo=new Todo
        {
            Title=title,
            Description=description
        };
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult>ToggleDone(int id)
    {
        var todo =await _context.Todos.FindAsync(id);
        if(todo==null)
        return NotFound();

        todo.IsDone=!todo.IsDone;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

}