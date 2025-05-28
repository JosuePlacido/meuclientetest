using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
	public interface IEntity
	{
		string Id { get; }
	}
	public abstract class Entity : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; protected set; }

		protected Entity()
		{
		}


		public override bool Equals(object obj)
		{
			if (obj is Entity entity)
			{
				return Id == entity.Id;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}

	public class InternalScoped : Entity
	{
		[Required]
		public string Name { get; protected set; }
		[Required]
		public string Code { get; protected set; }

		protected InternalScoped() { }
	}
}
