using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.RepositoryPattern;
using ServiceLayer.CustomerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "OnionArchitecture", Version = "v1" });
});

#region Connection String
builder.Services.AddDbContext<ApplicationDbContext>(item => item.UseSqlServer("name=ConnectionStrings:localOnionSql"));
#endregion

#region Services Injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<ICustomerService, CustomerService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.sjon", "OnionArchitecture v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
