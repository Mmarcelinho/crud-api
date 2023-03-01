namespace AspNetCore.WebApi.Controllers;

[ApiController]
[Route("v1")]
public class TodoController : ControllerBase
{

    [HttpGet]
    [Route("todo")]
    public async Task<IActionResult> Get(
        [FromServices] Context context,
        int skip = 0,
        int take = 25)
    {

        var total = await context.Todo.CountAsync();
        var todo = await context.Todo
        .AsNoTracking()
        .Skip(skip)
        .Take(take)
        .ToListAsync();

        return Ok(todo);

    }

    [HttpGet]
    [Route("todo/{id}")]
    public async Task<IActionResult> GetById(
        [FromServices] Context context, int id)
    {

        var todo = await context.Todo
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);

        return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    [Route("todo")]
    public async Task<IActionResult> Post(
        [FromServices] Context context,
        [FromBody] TodoInput model)
    {

        if (!ModelState.IsValid)
            return BadRequest();

        var todo = new Todo
        {
            Date = DateTime.Now,
            Done = false,
            Title = model.Title
        };

        await context.Todo.AddAsync(todo);
        var result = await context.SaveChangesAsync();
        return result > 0 ? Created($"/todo/{todo.Id}", new TodoOutput(todo.Id, todo.Title, todo.Date, todo.Done)) : BadRequest("Houve um problema ao salvar o registro");

    }

    [HttpPut("todo/{id}")]
    public async Task<IActionResult> Put(
        [FromServices] Context context,
        [FromBody] TodoInput model,
        [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var todo = await context.Todo.FirstOrDefaultAsync(x => x.Id == id);

        if (todo == null)
            return NotFound();

        todo.Title = model.Title;
        context.Todo.Update(todo);
        var result = await context.SaveChangesAsync();
        return result > 0 ? NoContent() : BadRequest("Houve um problema ao salvar o registro");

    }

    [HttpDelete("todo/{id}")]
    public async Task<IActionResult> Delete(
        [FromServices] Context context,
        [FromRoute] int id)
    {
        var todo = await context.Todo.FirstOrDefaultAsync(x => x.Id == id);

        if (todo == null)

            return NotFound();

        context.Todo.Remove(todo);
        var result = await context.SaveChangesAsync();
        return result > 0 ? NoContent() : BadRequest("Houve um problema ao salvar o registro");
    }

}
