using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Npgsql;
using System.Drawing.Text;
using APITrail;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TodoContextWorker>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<TodoContextKorona>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<TodoContextVaccination>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();









