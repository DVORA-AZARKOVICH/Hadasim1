using System;

using Microsoft.EntityFrameworkCore;


namespace TodoApi.Models;


public class TodoContextVaccination : DbContext
{
    public TodoContextVaccination(DbContextOptions<TodoContextVaccination> options)
        : base(options)
    {
    }
    public DbSet<Vaccination> TodoVaccination { get; set; } = null!;

}
