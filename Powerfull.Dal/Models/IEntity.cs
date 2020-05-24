using System;

namespace Powerfull.Dal.Models
{
	public interface IEntity<TIdType>
		where TIdType : IEquatable<TIdType>
	{
		TIdType Id { get; set; }
	}
}
