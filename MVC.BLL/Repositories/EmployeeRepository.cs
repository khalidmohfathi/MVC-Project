using MVC.BLL.Interfaces;
using MVC.DAL.Data.Contexts;
using MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.Repositories
{
	public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		public IQueryable<Employee> GetEmployeesByAddress(string address)
		{
			return _dbContext.Employees.Where(E => E.Address.ToLower().Contains(address.ToLower()) );
		}
	}
}
