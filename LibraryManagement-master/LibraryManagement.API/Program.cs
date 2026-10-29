using LibraryManagement.API.Services;
using LibraryManagement.Application.Services;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.Servisec.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString));

// خدمات الكتب الموجودة
builder.Services.AddScoped<IBookServices, BookServices>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// خدمات المستخدمين الجديدة
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FlutterLocal", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
        {
            return Uri.TryCreate(origin, UriKind.Absolute, out var uri)
                && (uri.Host == "localhost"
                    || uri.Host == "127.0.0.1");
        })
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FlutterLocal");
Directory.CreateDirectory(
    Path.Combine(app.Environment.ContentRootPath, "wwwroot"));

app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();