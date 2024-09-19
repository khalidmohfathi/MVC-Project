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

		public void Add(T item)
		{
			_dbContext.Add(item);
		}

		public void Delete(T item)
		{
			_dbContext.Remove(item);
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _dbContext.Set<T>().AsNoTracking().ToList();
		}

		public T GetById(int id)
		{
			return _dbContext.Set<T>().Find(id);
		}

		public void Update(T item)
		{
			_dbContext.Update(item);
		}
	}
}
