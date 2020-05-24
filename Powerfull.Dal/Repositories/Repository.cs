using NHibernate;
using NHibernate.Linq;
using Powerfull.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Powerfull.Dal.Repositories
{
	public abstract class Repository<TEntity, TIdType> : IRepository<TEntity, TIdType>
		where TEntity : class, IEntity<TIdType>
		where TIdType : IEquatable<TIdType>
	{
		#region Private Fields

		protected readonly ISession _session;

		#endregion

		#region Ctors

		public Repository(ISession session)
		{
			this._session = session;
		}

		#endregion

		#region Public Methods

		public TEntity FindById(TIdType id)
		{
			return this.Find(x => x.Id.Equals(id));
		}

		public async Task<TEntity> FindByIdAsync(TIdType id)
		{
			return await this.FindAsync(x => x.Id.Equals(id));
		}

		public TEntity Find(Func<TEntity, bool> predicate)
		{
			return this.GetModelIncludeFields()
				.FirstOrDefault(predicate);
		}

		public async Task<TEntity> FindAsync(
			Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
		{
			return await this.GetModelIncludeFields()
				.FirstOrDefaultAsync(predicate, cancellationToken);
		}

		public bool IsExistById(TEntity entity)
		{
			return this.FindById(entity.Id) != null;
		}

		public async Task<bool> IsExistByIdAsync(TEntity entity)
		{
			return await this.FindByIdAsync(entity.Id) != null;
		}

		public IEnumerable<TEntity> GetAll()
		{
			return this.GetModelIncludeFields();
		}

		public TEntity Create(TEntity entity)
		{
			this.ThrowExceptionIfExist(entity);

			if (!this.CanCreateEntity(entity))
			{
				return null;
			}

			this._session.Save(entity);
			return entity;
		}

		public async Task<TEntity> CreateAsync(TEntity entity)
		{
			this.ThrowExceptionIfExist(entity);

			if (!this.CanCreateEntity(entity))
			{
				return null;
			}

			await this._session.SaveAsync(entity);
			return entity;
		}

		public TEntity Update(TEntity entity)
		{
			TEntity foundEntity = this.ThrowExceptionIfNotExist(entity);

			if (!this.CanUpdateEntity(foundEntity))
			{
				return null;
			}

			this._session.Update(foundEntity);
			return foundEntity;
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			TEntity foundEntity = this.ThrowExceptionIfNotExist(entity);

			if (!this.CanUpdateEntity(foundEntity))
			{
				return null;
			}

			await this._session.UpdateAsync(foundEntity);
			return foundEntity;
		}

		public TEntity Delete(TEntity entity)
		{
			TEntity foundEntity = this.ThrowExceptionIfNotExist(entity);

			if (!this.CanDeleteEntity(foundEntity))
			{
				return null;
			}

			this._session.Delete(foundEntity);
			return foundEntity;
		}

		public async Task<TEntity> DeleteAsync(TEntity entity)
		{
			TEntity foundEntity = this.ThrowExceptionIfNotExist(entity);

			if (!this.CanDeleteEntity(foundEntity))
			{
				return null;
			}

			await this._session.DeleteAsync(foundEntity);
			return foundEntity;
		}

		#endregion

		#region Protected Methods

		protected virtual IQueryable<TEntity> GetModelIncludeFields()
		{
			return this._session.Query<TEntity>();
		}

		protected void ThrowExceptionIfExist(TEntity entity)
		{
			TEntity foundEntity = this.FindById(entity.Id);

			if (foundEntity != null)
			{
				throw new InvalidOperationException($"Such entity \'{entity.GetType().Name}\' exist yet at database");
			}
		}

		protected TEntity ThrowExceptionIfNotExist(TEntity entity)
		{
			TEntity foundEntity = this.FindById(entity.Id);

			if (foundEntity == null)
			{
				throw new InvalidOperationException($"Such entity \'{entity.GetType().Name}\' not exist at database");
			}

			return foundEntity;
		}

		protected virtual bool CanCreateEntity(TEntity entity) => true;

		protected virtual bool CanUpdateEntity(TEntity entity) => true;

		protected virtual bool CanDeleteEntity(TEntity entity) => true;

		#endregion
	}
}
