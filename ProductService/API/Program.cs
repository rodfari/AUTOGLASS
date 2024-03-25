using Application;
using Application.Ports;
using DbSqlLite;
using DbSqlLite.Repositories;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IProdutoManager, ProdutoManager>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

string connection = builder.Configuration.GetConnectionString("SqlLite");
builder.Services.AddDbContext<ProdutoDbContext>(opt => {
    opt.UseSqlite(connection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
