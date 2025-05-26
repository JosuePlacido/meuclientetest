using System;
using System.Collections.Generic;

namespace Application.DTO
{
	public class ValidationError
	{
		public string Key { get; private set; }
		public string Message { get; private set; }

		public ValidationError(string key, string message)
		{
			Key = key;
			Message = message;
		}
	}
	public class ValidationException : Exception
	{
		public ICollection<ValidationError> Errors { get; private set; }

		public ValidationException(string message)
			: base(message)
		{
			Errors = new List<ValidationError>();
		}

		public ValidationException(string message, params ValidationError[] errors)
			: base(message)
		{
			Errors = errors;
		}
	}
}
