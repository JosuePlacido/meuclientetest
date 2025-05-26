using Api.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Api.DAL
{
	public interface IDAO<T> where T : class, IEntity
	{
		Task<T[]> GetAll();
		Task<T> GetById(string id);
		Task<T> Add(T obj);
		T Update(T obj);
		T Delete(T obj);
		Task<IDbContextTransaction> BeginTtransaction();
		Task Commit(IDbContextTransaction transaction = null);
	}
}
