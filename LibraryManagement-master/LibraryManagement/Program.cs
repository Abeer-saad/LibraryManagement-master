
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LibraryManagement.Application;
using LibraryManagement.Application.Services;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Servisec.Base;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddScoped<IBookServices, BookServices>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

    .AddCookie(options =>

    {

        options.LoginPath = "/Account/Login";

        options.AccessDeniedPath = "/Account/AccessDenied";

        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

        options.Cookie.HttpOnly = true;

        options.SlidingExpiration = true;

    })
    

.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>

 {

     options.TokenValidationParameters = new TokenValidationParameters

     {

         ValidateIssuer = true,

         ValidateAudience = true,

         ValidateLifetime = true,

         ValidateIssuerSigningKey = true,

         ValidIssuer = builder.Configuration["Jwt:Issuer"],

         ValidAudience = builder.Configuration["Jwt:Audience"],

         IssuerSigningKey = new SymmetricSecurityKey(

             Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)

         )

     };

 });

var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseSqlServer(conectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
