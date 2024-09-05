using Microsoft.EntityFrameworkCore;
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
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly AppDbContext _dbContext;

		public DepartmentRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public int Add(Department department)
		{
			_dbContext.Departments.Add(department);
			return _dbContext.SaveChanges();
		}

		public int Delete(Department department)
		{
			_dbContext.Departments.Remove(department);
			return _dbContext.SaveChanges();
		}

		public IEnumerable<Department> GetAll()
		{
			return _dbContext.Departments.AsNoTracking().ToList();
		}

		public Department GetById(int id)
		{
			return _dbContext.Departments.Find(id);
		}

		public int Update(Department department)
		{
			_dbContext.Departments.Update(department);
			return _dbContext.SaveChanges();
		}
	}
}
