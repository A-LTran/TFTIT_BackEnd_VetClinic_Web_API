﻿using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace TFTIC_BackEnd_VetClinic_Web_API.Tools.ExceptionHandler
{
    public class BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError($"Bad request exception: {exception.Message}");

            if (exception is not BadHttpRequestException badRequestException)
            {
                return false;
            }

            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Title = "Bad Request",
                Message = badRequestException.Message
            };

            httpContext.Response.StatusCode = errorResponse.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}
