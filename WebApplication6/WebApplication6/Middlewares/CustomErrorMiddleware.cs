namespace WebApplication6.Middlewares;

public class CustomErrorMiddleware
{
    private readonly RequestDelegate _next;

    public CustomErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? "";


        if (path.StartsWith("/test-error/418"))
        {
            context.Response.StatusCode = 418;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync("<h2>Помилка 418: I'm a teapot</h2><p>Я чайник! Обробка через Custom Middleware</p>");
            return;
        }

        if (path.StartsWith("/test-error/429"))
        {
            context.Response.StatusCode = 429;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync("<h2>Помилка 429: Забагато запитів</h2><p>Спробуйте пізніше. Обробка через Custom Middleware</p>");
            return;
        }

        await _next(context);


        if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync("<h2>Помилка 404: Сторінку не знайдено</h2><p>Обробка через Custom Middleware</p>");
        }
    }
}