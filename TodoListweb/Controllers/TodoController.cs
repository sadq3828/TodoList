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

    // نمایش لیست Todos
   public async Task<IActionResult> Index(int? editId)
{
    var todos = await _context.Todos.OrderByDescending(t => t.CreatedAt).ToListAsync();
    
    Todo editTodo = null;
    if(editId.HasValue)
    {
        editTodo = await _context.Todos.FindAsync(editId.Value);
    }

    ViewBag.EditTodo = editTodo; // اطلاعات مورد ویرایش
    return View(todos);
}

    // ایجاد Todo جدید
    [HttpPost]
    public async Task<IActionResult> Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            return RedirectToAction(nameof(Index));

        var todo = new Todo
        {
            Title = title,
            Description = description
        };
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // تغییر وضعیت Done
    [HttpPost]
    public async Task<IActionResult> ToggleDone(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        todo.IsDone = !todo.IsDone;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // حذف Todo
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: نمایش فرم ویرایش
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        return View(todo); // ویو Edit.cshtml
    }

    // POST: ذخیره تغییرات
   [HttpPost]
public async Task<IActionResult> Edit(int id, string title, string description)
{
    var todo = await _context.Todos.FindAsync(id);
    if(todo != null)
    {
        todo.Title = title;
        todo.Description = description;
        await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
}
}
