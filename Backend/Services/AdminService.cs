/*using System;
using Microsoft.EntityFrameworkCore;
using ProjektST2.DTO;
using ProjektST2.Entities;

namespace ProjektST2.Services
{
	public class AdminService: IAdminService
	{
        private readonly AplikacjaDBContext _context;
        public AdminService(AplikacjaDBContext context)
        {
            _context = context;
        }

        public List<WydatekDTO> GetExpenses()
        {
            var expenses = _context.Wydatek.ToList();
            var expenseDTOs = new List<WydatekDTO>();
            foreach (var expense in expenses)
            {
                expenseDTOs.Add(new WydatekDTO
                {
                    Name = expense.Name,
                    ExpenseTime = expense.ExpenseTime,
                    Price = expense.Price
                });
            }
            return expenseDTOs;
        }

        public Wydatek GetExpense(int id)
        {
            try
            {
                var wydatek = _context.Wydatek.FirstOrDefault(x => x.Id == id);
                return wydatek;
            }
            catch
            {
                return null;
            }
        }

        public string Delete(int id)
        {
            try
            {
                var wydatek = _context.Wydatek.FirstOrDefault(x => x.Id == id);
                wydatek.Price = 0;
                _context.Wydatek.Update(wydatek);
                _context.SaveChanges();
                return "wydatek został wyzerowany";
            }
            catch (Exception)
            {
                return "Wewnętrzny błąd, sprawdź czy id jest prawidłowe";
            }
        }

        public void Update(Wydatek wydatek)
        {
            try
            {
                _context.Wydatek.Update(wydatek);
                _context.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

  }
}

*/