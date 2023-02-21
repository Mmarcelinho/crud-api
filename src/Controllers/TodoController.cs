using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCore.WebApi.Models;
using AspNetCore.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using AspNetCore.WebApi.ViewModel;

namespace AspNetCore.WebApi.Controllers
{
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
            [FromBody] TodoViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new Todo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

             try
             {

                await context.Todo.AddAsync(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
             } 

             catch(Exception e)
             {
               return NotFound(e);
             }

        }

        [HttpPut("todo/{id}")]
        public async Task<IActionResult> Put(
            [FromServices] Context context,
            [FromBody] TodoViewModel model,
            [FromRoute] int id)

        {

            if (!ModelState.IsValid)
                return BadRequest();


            var todo = await context.Todo.FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try
            {

                todo.Title = model.Title;
                context.Todo.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpDelete("todo/{id}")]
        public async Task<IActionResult> Delete(
            [FromServices] Context context,
            [FromRoute] int id)
        {
        
            var todo = await context.Todo.FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)

                return NotFound();
            try
            {
               
                context.Todo.Remove(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }


        }


    }
}