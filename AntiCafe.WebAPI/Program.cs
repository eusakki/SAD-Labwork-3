using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AntiCafe.DAL.Data;
using AntiCafe.DAL.UnitOfWork;
using AntiCafe.BLL.Services;
using AntiCafe.BLL.Interfaces;
using AntiCafe.BLL.Mapping;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Connecting Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // UoW
    container.RegisterType<UnitOfWork>()
        .As<IUnitOfWork>()
        .InstancePerLifetimeScope();

    // Services
    container.RegisterType<RoomService>().As<IRoomService>();
    container.RegisterType<BookingService>().As<IBookingService>();
    container.RegisterType<ActivityService>().As<IActivityService>();
});

// EF Core
builder.Services.AddDbContext<AntiCafeDbContext>(opt =>
    opt.UseInMemoryDatabase("AntiCafeDB"));

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// DataSeeder
using (var scope = app.Services.CreateScope())
{
    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    await DataSeeder.SeedAsync(uow);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
