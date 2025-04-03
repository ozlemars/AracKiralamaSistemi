using AracKiralamaSistemi.Models.Dtos.Brand;
using AracKiralamaSistemi.Models.Dtos.Car;
using AracKiralamaSistemi.Models.Dtos.Fuel;
using AracKiralamaSistemi.Models.Dtos.Transmission;
using AracKiralamaSistemi.Repository.Contexts;
using AracKiralamaSistemi.Service.Abstract;
using AracKiralamaSistemi.Service.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AracKiralamaSistemi.Models.Dtos.Color;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<IColorService, ColorService>();
builder.Services.AddTransient<IFuelService, FuelService>();
builder.Services.AddTransient<ITransmissionService, TransMissionService>();


builder.Services.AddControllersWithViews().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CarDtoValidator>());
builder.Services.AddControllersWithViews().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<BrandDtoValidator>());
builder.Services.AddControllersWithViews().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<ColorDtoValidator>());
builder.Services.AddControllersWithViews().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<FuelDtoValidator>());
builder.Services.AddControllersWithViews().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<TransmissionDtoValidator>());

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
