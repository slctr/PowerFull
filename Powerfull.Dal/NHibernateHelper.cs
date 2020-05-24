using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Engine;
using NHibernate.Persister.Entity;
using NHibernate.Proxy;
using NHibernate.Tool.hbm2ddl;
using Powerfull.Dal.Maps;
using Powerfull.Dal.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Powerfull.Dal
{
	public class NHibernateHelper
	{
		private static ISessionFactory _sessionFactory;


		public static ISession OpenSession()
		{
			return NHibernateHelper.SessionFactory.OpenSession();
		}

		#region Dirty Pattern

		/// <summary>
		/// Get dirty objects
		/// </summary>
		/// <returns>Dirty objects</returns>
		/// <remarks>
		/// Code was taken from https://stackoverflow.com/questions/19788665/how-to-get-dirty-entities-in-nhibernate-using-c-sharp-and-fluentnhibernate
		/// </remarks>
		public static IEnumerable<object> GetDirtyObjects(ISession session)
		{
			List<object> dirtyObjects = new List<object>();
			ISessionImplementor sessionImplementor = session.GetSessionImplementation();
			IEnumerable entityEntries = sessionImplementor.PersistenceContext.EntityEntries.Values;

			foreach (EntityEntry entityEntry in entityEntries)
			{
				object[] loadedState = entityEntry.LoadedState;
				object entity = sessionImplementor.PersistenceContext.GetEntity(entityEntry.EntityKey);
				object[] currentState = entityEntry.Persister.GetPropertyValues(entity);
				int[] properties = entityEntry.Persister.FindDirty(currentState, loadedState, entity, sessionImplementor);

				if (properties != null)
				{
					dirtyObjects.Add(entityEntry);
				}
			}

			return dirtyObjects;
		}

		/// <summary>
		/// Get is property dirty
		/// </summary>
		/// <param name="session"></param>
		/// <param name="entity"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		/// <remarks>
		/// Code was taken from https://nhibernate.info/doc/howto/various/finding-dirty-properties-in-nhibernate
		/// </remarks>
		public static bool IsDirtyProperty(
			ISession session, object entity, string propertyName)
		{
			ISessionImplementor sessionImpl = session.GetSessionImplementation();
			IPersistenceContext persistenceContext = sessionImpl.PersistenceContext;
			EntityEntry oldEntry = persistenceContext.GetEntry(entity);
			string className = oldEntry.EntityName;

			IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);

			if ((oldEntry == null) && (entity is INHibernateProxy))
			{
				INHibernateProxy proxy = entity as INHibernateProxy;
				object obj = sessionImpl.PersistenceContext.Unproxy(proxy);
				oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);
			}

			object[] oldState = oldEntry.LoadedState;
			object[] currentState = persister.GetPropertyValues(entity);
			int[] dirtyProps = persister.FindDirty(currentState, oldState, entity, sessionImpl);
			int index = Array.IndexOf(persister.PropertyNames, propertyName);

			bool isDirty = (dirtyProps != null) ? (Array.IndexOf(dirtyProps, index) != -1) : false;
			return isDirty;
		}

		#endregion Dirty Pattern


		private static ISessionFactory SessionFactory
		{
			get
			{
				if (NHibernateHelper._sessionFactory == null)
				{
					NHibernateHelper.InitializeSessionFactory();
				}

				return NHibernateHelper._sessionFactory;
			}
		}

		private static void InitializeSessionFactory()
		{
			string connectionString = "Server=(localdb)\\mssqllocaldb;Database=PowerfullTool;Trusted_Connection=True;MultipleActiveResultSets=true";
			Configuration configuration = Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<LoanApplicationModelMap>())
				.BuildConfiguration();

			SchemaExport exporter = new SchemaExport(configuration);

			// To be not remove data switch 2nd param to false
			exporter.Execute(true, false, false);

			NHibernateHelper._sessionFactory = configuration.BuildSessionFactory();

			// Comment to don't use seed temp data
			NHibernateHelper.Seed();
		}

		private static void Seed()
		{
			using (ISession session = NHibernateHelper.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.New, ApplicantDetails = "Something about application 1", AmountRequested = 123, AmountGranted = 12, DateOfSubmission = DateTime.Now - TimeSpan.FromDays(5) });
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.Started, ApplicantDetails = "Something about application 2", AmountRequested = 456, AmountGranted = 345, DateOfSubmission = DateTime.Now - TimeSpan.FromDays(1) });
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.Verified, ApplicantDetails = "Something about application 3", AmountRequested = 789, AmountGranted = 789, DateOfSubmission = DateTime.Now - TimeSpan.FromDays(365) });
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.Accepted, ApplicantDetails = "Something about application 4", AmountRequested = 123456789, AmountGranted = 0, DateOfSubmission = DateTime.Now - TimeSpan.FromHours(8) });
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.Verified, ApplicantDetails = "Something about application 5", AmountRequested = 122563, AmountGranted = 8426, DateOfSubmission = DateTime.Now - TimeSpan.FromHours(2) });
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.Verified, ApplicantDetails = "Something about application 6", AmountRequested = 463456, AmountGranted = 96433, DateOfSubmission = DateTime.Now - TimeSpan.FromDays(10) });
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.Verified, ApplicantDetails = "Something about application 7", AmountRequested = 786589, AmountGranted = 97211, DateOfSubmission = DateTime.Now - TimeSpan.FromDays(62) });
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.Accepted, ApplicantDetails = "Something about application 8", AmountRequested = 743, AmountGranted = 1, DateOfSubmission = DateTime.Now - TimeSpan.FromHours(1) });
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.New, ApplicantDetails = "Something about application 9", AmountRequested = 825, AmountGranted = 479, DateOfSubmission = DateTime.Now - TimeSpan.FromHours(92) });
					session.Save(new LoanApplicationModel { ApplicationStatus = ApplicationStatusEnum.Started, ApplicantDetails = "Something about application 10", AmountRequested = 95, AmountGranted = 53, DateOfSubmission = DateTime.Now - TimeSpan.FromDays(8) });

					transaction.Commit();
				}
			}
		}
	}
}
