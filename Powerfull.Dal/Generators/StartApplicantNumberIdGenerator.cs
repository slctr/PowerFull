using NHibernate.Engine;
using NHibernate.Id;
using System;

namespace Powerfull.Dal.Generators
{
	/// <summary>
	/// Code was taken from https://nhibernate.info/doc/howto/various/creating-a-custom-id-generator-for-nhibernate
	/// </summary>
	public class StartApplicantNumberIdGenerator : TableGenerator
	{
		private const long c_SeedValue = 4500000;

		public override object Generate(ISessionImplementor session, object obj)
		{
			long counter = Convert.ToInt32(base.Generate(session, obj));
			return counter + c_SeedValue;
		}
	}
}
