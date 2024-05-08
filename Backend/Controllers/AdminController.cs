using System;
using Microsoft.AspNetCore.Mvc;
using ProjektST2.DTO;
using ProjektST2.Entities;
using ProjektST2.Services;

namespace ProjektST2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class AdminController : ControllerBase
    {
        private AplikacjaDBContext _context;

        public AdminController(AplikacjaDBContext context)
        {
            _context = context;
        }

        [HttpPut]
        public void Update([FromBody] Wydatek wydatek)
        {
            var a = new AdminService(_context);
            a.Update(wydatek);
        }

        [HttpDelete]
        public string Delete(int id)
        {
            var a = new AdminService(_context);
            return a.Delete(id);
        }

        [HttpGet]
        public List<WydatekDTO> Expenses()
        {
            var a = new AdminService(_context);
            return a.GetExpenses();
        }

        [HttpGet]
        public Wydatek Expense(int id)
        {
            var a = new AdminService(_context);
            return a.GetExpense(id);
        }
    }
}

