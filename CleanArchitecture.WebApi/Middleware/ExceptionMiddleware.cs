using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.WebApi.Middleware
{
    public sealed class ExceptionMiddleware : IMiddleware
    {

        private readonly AppDbContext _context;

        public ExceptionMiddleware(AppDbContext context)
        {
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionsToDatabase(ex, context.Request);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            if (ex.GetType() == typeof(ValidationException))
            {
                return context.Response.WriteAsync(new ValidationErrorDetails
                {
                    Errors = ((ValidationException)ex).Errors.Select(x => x.PropertyName),
                    StatusCode = 403,
                }.ToString());
            }

            return context.Response.WriteAsync(new ErrorResult
            {
                Message = ex.Message,
                StatusCode = context.Response.StatusCode,
            }.ToString());
        }

        private async Task LogExceptionsToDatabase(Exception ex, HttpRequest request)
        {
            ErrorLog errorLog = new()
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                RequestPath = request.Path,
                RequestMethod = request.Method,
                Timestap = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
            };

            // Loglama işlemi yapılacak.
            await _context.Set<ErrorLog>().AddAsync(errorLog,default);
            await _context.SaveChangesAsync(default);
        }
    }
}
