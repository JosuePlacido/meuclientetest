using System;
using System.Linq;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

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
		}
		else
		{
			_logger.LogError(context.Exception, "Erro ao processar a requisição.");
			context.Result = new ObjectResult("Ocorreu um erro inesperado.")
			{
				StatusCode = 500
			};
			context.ExceptionHandled = true;
		}
	}
}
