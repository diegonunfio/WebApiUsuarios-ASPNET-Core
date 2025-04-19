using Microsoft.EntityFrameworkCore;
using WebApiUsers.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbUserContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("connectionDB")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // <-- Este es vital



builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//app.MapGet("/", (HttpContext context) =>
//{
//    context.Response.Redirect("/swagger/index.html");
//    return Task.CompletedTask;
//});

app.Use(async (context, next) =>
{ 
    if(context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html", permanent: false);
        return;
    }
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

