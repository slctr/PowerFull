using System;

namespace Powerfull.Dal.Models
{
	public interface ISoftDeletable
	{
		DateTime? DeletedDate { get; set; }
	}
}
