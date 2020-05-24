using Powerfull.Dal.Models;
using System;
using System.Collections.Generic;

namespace Powerfull.Dal.Repositories
{
	public interface ISoftDeleteRepository<TEntity, TIdType>
		: IRepository<TEntity, TIdType>
		where TEntity : class, IEntity<TIdType>
		where TIdType : IEquatable<TIdType>
	{
		IEnumerable<TEntity> GetAllWothoutDeleted();
	}
}
