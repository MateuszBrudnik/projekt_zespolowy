using System;
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
        public ActionResult<IEnumerable<Income>> GetIncomes()
        {
            return Ok(_incomeService.GetIncomes());
        }

        [HttpGet("{id}")]
        public ActionResult<Income> GetIncome(int id)
        {
            return Ok(_incomeService.GetIncomeById(id));
        }

        [HttpPost]
        public IActionResult AddIncome([FromBody] Income income)
        {
            _incomeService.AddIncome(income);
            return CreatedAtAction(nameof(GetIncome), new { id = income.Id }, income);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateIncome(int id, [FromBody] Income income)
        {
            if (id != income.Id)
            {
                return BadRequest();
            }
            _incomeService.UpdateIncome(income);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIncome(int id)
        {
            _incomeService.DeleteIncome(id);
            return NoContent();
        }
    }
}

