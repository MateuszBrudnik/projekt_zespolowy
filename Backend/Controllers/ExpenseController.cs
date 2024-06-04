using System;
using Microsoft.AspNetCore.Mvc;
using Projekt.Entities;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Expense>> GetExpenses()
        {
            return Ok(_expenseService.GetExpenses());
        }

        [HttpGet("{id}")]
        public ActionResult<Expense> GetExpense(int id)
        {
            var expense = _expenseService.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPost]
        public IActionResult AddExpense([FromBody] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _expenseService.AddExpense(expense);
                return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, [FromBody] Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _expenseService.UpdateExpense(expense);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            var expense = _expenseService.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }
            _expenseService.DeleteExpense(id);
            return NoContent();
        }
    }
}

