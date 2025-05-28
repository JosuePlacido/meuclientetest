using Api.DTO;
using Api.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Api.DAL
{
	public interface IDAO<T> where T : class, IEntity
	{
		Task<T[]> GetAll();
		Task<PaginationDTO<T>> List(int page, int take);
		Task<T> GetById(string id);
		Task<bool> Exists(string id);
		Task<T> Add(T obj);
		T Update(T obj);
		T Delete(T obj);
		Task<IDbContextTransaction> BeginTtransaction();
		Task Commit(IDbContextTransaction transaction = null);
	}
}
