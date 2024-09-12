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
	public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
	{
		public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
		{
		}
	}
}
