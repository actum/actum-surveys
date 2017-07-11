using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Web
{
    public class ClientIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly string _cookieName = "CID";

        public static readonly object ContextClientId = new object();

        public ClientIdMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ClientIdMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            string valueStr;
            Guid value = Guid.Empty;

            // try parse
            if (context.Request.Cookies.TryGetValue(_cookieName, out valueStr))
            {
                Guid.TryParse(valueStr, out value);
            }

            if (value == Guid.Empty)
            {
                value = Guid.NewGuid();
                context.Response.Cookies.Append(
                    _cookieName,
                    value.ToString("N"),
                    new CookieOptions()
                    {
                        Expires = DateTimeOffset.Now.AddYears(1)
                    }
                );
                _logger.LogDebug($"New client Id: { value }");
            }

            context.Items[ClientIdMiddleware.ContextClientId] = value;

            await _next.Invoke(context);
        }
    }
}
