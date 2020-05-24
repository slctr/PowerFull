using NHibernate;
using Powerfull.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Powerfull.Dal.Repositories
{
	public abstract class SoftDeleteRepository<TEntity, TIdType>
		: Repository<TEntity, TIdType>, ISoftDeleteRepository<TEntity, TIdType>
		where TEntity : class, ISoftDeletable, IEntity<TIdType>
		where TIdType : IEquatable<TIdType>
	{
		#region Ctors

		public SoftDeleteRepository(
			ISession session)
			: base(session)
		{
		}

		#endregion

		#region Public Methods

		public TEntity SoftDelete(TEntity entity)
		{
			TEntity foundEntity = this.ThrowExceptionIfNotExist(entity);
			if (foundEntity.DeletedDate != null)
			{
				throw new ArgumentException($"Argument {nameof(entity)} has been deleted yet");
			}

			foundEntity.DeletedDate = DateTime.UtcNow;
			return foundEntity;
		}

		public IEnumerable<TEntity> GetAllWothoutDeleted()
		{
			IEnumerable<TEntity> allEntities = this.GetAll()
				.Where(x => x.DeletedDate == null);

			return allEntities;
		}

		#endregion

		#region Protected Methods

		protected override bool CanCreateEntity(TEntity entity) => entity.DeletedDate == null;

		protected override bool CanUpdateEntity(TEntity entity) => entity.DeletedDate == null;

		#endregion
	}
}
