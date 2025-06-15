using AuthSSO.Common.Constants;

namespace AuthSSO.Middlewares
{
    public class BearerMiddleware
    {
        private readonly RequestDelegate _next;

        public BearerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies[Constants.AccessToken];
            if (token is null)
            {
                await _next(context);
            }
            else 
            {
                context.Request.Headers.Add(Constants.HeaderAuthorization, Constants.Bearer + " " + token);
                await _next(context);
                if (context.Response.Headers.ContainsKey(Constants.HeaderAuthorization))
                {
                    context.Response.Headers.Remove(Constants.HeaderAuthorization);
                }
            }
        }
    }
}
