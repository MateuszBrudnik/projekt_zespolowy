using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projekt.Entities;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomesController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _incomeService.GetIncomesAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncome(int id)
        {
            var income = await _incomeService.GetIncomeByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }
            return Ok(income);
        }

        [HttpPost]
        public async Task<ActionResult> AddIncome([FromBody] Income income)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            income.UserId = userId;

            await _incomeService.AddIncomeAsync(income);
            return CreatedAtAction(nameof(GetIncome), new { id = income.Id }, income);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] Income income)
        {
            if (id != income.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _incomeService.UpdateIncomeAsync(income);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var income = await _incomeService.GetIncomeByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            await _incomeService.DeleteIncomeAsync(id);
            return NoContent();
        }
    }
}
