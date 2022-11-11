using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using mytask.ExceptionBase;

namespace mytask.Handler
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string path = context.Request.Path;
            string method = context.Request.Method;
            string date = DateTime.Now.ToString();

            var error = new ResponseException();
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
                NotFoundException notFoundException = (NotFoundException)exception;
                error = ConvertToExceptionResponse(notFoundException.code, notFoundException.message);
            }
            else if (exception is ValidationException)
            {
                code = HttpStatusCode.BadRequest;
                ValidationException validationException = (ValidationException)exception;
                error = ConvertToExceptionResponse(validationException.code, validationException.message);
            }
            else if (exception is UndefindedTypeException)
            {
                code = HttpStatusCode.BadRequest;
                UndefindedTypeException undefindedTypeException = (UndefindedTypeException)exception;
                error = ConvertToExceptionResponse(undefindedTypeException.code, undefindedTypeException.message);
            }
            else if (exception is AggregateException)
            {
                AggregateException aggregateException = (AggregateException)exception;
                foreach (Exception inner in aggregateException.InnerExceptions)
                {
                    if (inner is NotFoundException)
                    {
                        code = HttpStatusCode.BadRequest;
                        NotFoundException notFoundException = (NotFoundException)inner;
                        error = ConvertToExceptionResponse(notFoundException.code, notFoundException.message);
                    }
                    else if (inner is ValidationException)
                    {
                        code = HttpStatusCode.BadRequest;
                        ValidationException validationException = (ValidationException)inner;
                        error = ConvertToExceptionResponse(validationException.code, validationException.message);
                    }
                    else
                    {
                        code = HttpStatusCode.InternalServerError;
                        error = ConvertToExceptionResponse("999999", exception.Message);
                    }
                }
            }
            else
            {
                code = HttpStatusCode.InternalServerError;
                error = ConvertToExceptionResponse("999999", exception.Message);

                _logger.LogError(exception, " Severity : Error \r\n Method : {method} \r\n Request : {path} \r\n Date-Time : {date}", method, path, date);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }

        private ResponseException ConvertToExceptionResponse(string code, string message)
        {
            return new ResponseException
            {
                errorCode = code,
                errorMessage = message
            };
        }
    }
}
