using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Api.ActionFilters
{

	public class ValidationExceptionFilter : IExceptionFilter
	{

		private readonly ILogger<ValidationExceptionFilter> _logger;

		public ValidationExceptionFilter(ILogger<ValidationExceptionFilter> logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext context)
		{
			if (context.Exception is ValidationException ex)
			{
				foreach (var error in ex.Errors)
				{
					context.ModelState.AddModelError(error.Key, error.Message);
				}

				context.Result = new BadRequestObjectResult(context.ModelState);
				context.ExceptionHandled = true;
				return;
			}
			if (context.Exception is BusinesRuleException brex)
			{
				context.Result = new ObjectResult(new
				{
					message = brex.Message,
					status = 400,
					data = brex.Entity
				})
				{
					StatusCode = 400
				};

				context.ExceptionHandled = true;
				return;
			}
			_logger.LogError(context.Exception, context.Exception.Message);
			context.Result = new ObjectResult("Ocorreu um erro inesperado.")
			{
				StatusCode = 500
			};
			context.ExceptionHandled = true;
		}
	}

}
