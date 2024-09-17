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
	public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
	{
		private protected readonly AppDbContext _dbContext;

		public GenericRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public int Add(T item)
		{
			_dbContext.Add(item);
			return _dbContext.SaveChanges();
		}

		public int Delete(T item)
		{
			_dbContext.Remove(item);
			return _dbContext.SaveChanges();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _dbContext.Set<T>().AsNoTracking().ToList();
		}

		public T GetById(int id)
		{
			return _dbContext.Set<T>().Find(id);
		}

		public int Update(T item)
		{
			_dbContext.Update(item);
			return _dbContext.SaveChanges();
		}
	}
}
