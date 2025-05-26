using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Api.DAL
{
	public abstract class DAOBase<T> : IDAO<T> where T : class, IEntity
	{
		protected readonly Context _context;

		public DAOBase(Context context)
		{
			_context = context;
		}
		public virtual async Task<T[]> GetAll()
		{
			return await _context.Set<T>().AsNoTracking().ToArrayAsync();
		}
		public virtual async Task<T> GetById(string id)
		{
			return await _context.Set<T>().FindAsync(id);
		}
		public virtual async Task<T> Add(T obj)
		{
			await _context.Set<T>().AddAsync(obj);
			return obj;
		}

		public virtual T Update(T obj)
		{
			_context.Set<T>().Update(obj);
			return obj;
		}

		public virtual T Delete(T obj)
		{
			_context.Set<T>().Remove(obj);
			return obj;
		}

		public virtual async Task Commit(IDbContextTransaction transaction = null)
		{
			await _context.SaveChangesAsync();
			if (transaction != null)
			{
				transaction.Commit();
			}
		}

		public async Task<IDbContextTransaction> BeginTtransaction()
		{
			return await _context.Database.BeginTransactionAsync();
		}
	}
}


