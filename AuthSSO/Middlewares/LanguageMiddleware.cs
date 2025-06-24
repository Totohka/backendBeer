using AuthSSO.Common.Constants;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AuthSSO.Middlewares
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var cookie = context.Request.Cookies[Constants.CookieCulture];
            if (cookie is null)
            {
                // Если null, то устанавливаем язык на ru
                var cultureInfo = new CultureInfo("ru");
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;

                await _next(context);

                context.Response.Cookies.Append(
                Constants.CookieCulture,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("ru")),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            }
            else
            {
                // Если не null, то ставим язык на тот, который в куки
                try
                {
                    var cookieParse = CookieRequestCultureProvider.ParseCookieValue(cookie);
                    string lang = cookieParse.Cultures[0].Value;
                    var cultureInfo = new CultureInfo(lang);
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                }
                catch (Exception ex)
                {
                    throw;
                }
                
                await _next(context);
            }
        }
    }
}
