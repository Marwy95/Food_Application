﻿using Food_Application.Enums;
using Food_Application.Exceptions;
using Food_Application.ViewModels;

namespace Food_Application.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = "Error Occured";
                ErrorCode errorCode = ErrorCode.UnKnown;

                if (ex is BusinessException businessException)
                {
                    message = businessException.Message;
                    errorCode = businessException.ErrorCode;
                }
                //else
                //{
                //    File.WriteAllText("D:\\Log.txt", $"Error happened: {ex.Message}");
                //}

                var result = ResultViewModel<bool>.Faliure(errorCode, message);

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
