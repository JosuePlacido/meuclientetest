using System;
using Api.Models;

namespace Application.Exceptions
{
	public class BusinesRuleException : Exception
	{
		public IEntity Entity { get; set; }


		public BusinesRuleException(string message, IEntity entity) : base(message)
		{
			Entity = entity;
		}
	}
}
