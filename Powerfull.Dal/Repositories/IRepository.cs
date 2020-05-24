using Powerfull.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Powerfull.Dal.Repositories
{
	public interface IRepository<TEntity, TIdType>
		where TEntity : class, IEntity<TIdType>
		where TIdType : IEquatable<TIdType>
	{
		#region Methods

		TEntity FindById(TIdType id);

		Task<TEntity> FindByIdAsync(TIdType id);

		TEntity Find(Func<TEntity, bool> predicate);

		Task<TEntity> FindAsync(
		   Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

		bool IsExistById(TEntity entity);

		Task<bool> IsExistByIdAsync(TEntity entity);

		IEnumerable<TEntity> GetAll();

		TEntity Create(TEntity entity);

		Task<TEntity> CreateAsync(TEntity entity);

		TEntity Delete(TEntity entity);

		Task<TEntity> DeleteAsync(TEntity entity);

		TEntity Update(TEntity entity);

		Task<TEntity> UpdateAsync(TEntity entity);

		#endregion
	}
}
