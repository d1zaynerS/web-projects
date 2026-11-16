using WebApplication6.Middlewares;
using WebApplication6.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<MovieService>(client =>
{
    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
});

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<IUserReader>(sp => sp.GetRequiredService<UserService>());
builder.Services.AddSingleton<IUserWriter>(sp => sp.GetRequiredService<UserService>());
builder.Services.AddSingleton<IUserNotifier>(sp => sp.GetRequiredService<UserService>());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<CustomErrorMiddleware>();

app.UseRouting();
app.UseAuthorization();

app.MapGet("/users/{id:int}", (int id, IUserReader reader) =>
    Results.Ok(reader.GetUserName(id)));

app.MapPut("/users/{id:int}", (int id, string name, IUserWriter writer) =>
{
    writer.UpdateUserName(id, name);
    return Results.Ok($"Имя пользователя с ID {id} обновлено на '{name}'");
});

app.MapPost("/users/{id:int}/notify", (int id, string msg, IUserNotifier notifier) =>
{
    notifier.Notify(id, msg);
    return Results.Ok($"Уведомление успешно отправлено пользователю с ID {id}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();