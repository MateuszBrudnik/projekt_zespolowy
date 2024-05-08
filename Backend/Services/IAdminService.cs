using System;
using ProjektST2.DTO;
using ProjektST2.Entities;

namespace ProjektST2.Services
{
	public interface IAdminService
	{
		public List<WydatekDTO> GetExpenses();
		public string Delete(int id);
		public void Update(Wydatek wydatek);
	}
}

