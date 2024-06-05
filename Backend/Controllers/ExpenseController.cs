using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Projekt.Entities;
using Projekt.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Projekt.Data;
using Microsoft.AspNetCore.Authorization;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly ExpenseContext _context;

        public ExpenseController(IExpenseService expenseService, ExpenseContext context)
        {
            _expenseService = expenseService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _expenseService.GetExpensesAsync(userId));
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            expense.UserId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _expenseService.AddExpenseAsync(expense);
            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                await _expenseService.UpdateExpenseAsync(expense);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            await _expenseService.DeleteExpenseAsync(id);
            return NoContent();
        }
    }
}
