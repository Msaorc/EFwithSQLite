using System.Runtime.Intrinsics.X86;
using EFWithSQLite.Data;
using Microsoft.AspNetCore.Mvc;
using EFWithSQLite.ViewModels;
using Microsoft.EntityFrameworkCore;
using EFWithSQLite.Models;

namespace EFWithSQLite.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ExpenseController : ControllerBase
    {
        [HttpGet]
        [Route("Expenses")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var expenses = await context
                .Expenses
                .AsNoTracking()
                .ToListAsync();

            return Ok(expenses);
        }

        [HttpGet]
        [Route("Expenses/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var expense = await context
                .Expenses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return expense == null ? NotFound() : Ok(expense);
        }

        [HttpPost]
        [Route("expenses")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateExpenseViewModel expenseViewModel
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var expense = new Expense
            {
                Description = expenseViewModel.Description,
                Value = expenseViewModel.Value,
                Type = expenseViewModel.Type
            };

            try
            {
                await context.Expenses.AddAsync(expense);
                await context.SaveChangesAsync();
                return Created($"v1/expenses/{expense.Id}", expense);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("expenses/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateExpenseViewModel expenseViewModel,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var expense = await context.Expenses.FirstOrDefaultAsync(x => x.Id == id);

            if (expense == null)
                return NotFound();

            try
            {
                expense.Description = expenseViewModel.Description;
                expense.Type = expenseViewModel.Type;
                expense.Value = expenseViewModel.Value;

                context.Expenses.Update(expense);
                await context.SaveChangesAsync();
                return Ok(expense);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("Expenses/{id}")]
        public async Task<IActionResult> Delete(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var expense = await context
            .Expenses
            .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Expenses.Remove(expense);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}