using Tutorial11.DAL;
using Microsoft.EntityFrameworkCore;
using Tutorial11.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddScoped<IHospitalService, HospitalService>();
// builder.Services.AddScoped<IClientsService, ClientsService>();
// builder.Services.AddScoped<IVisitsService, VisitService>();
builder.Services.AddDbContext<HospitalDBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();