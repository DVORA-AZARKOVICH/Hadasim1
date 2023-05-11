using System;

using Microsoft.EntityFrameworkCore;


namespace TodoApi.Models;


public class TodoContextWorker : DbContext
{
    public TodoContextWorker(DbContextOptions<TodoContextWorker> options)
        : base(options)
    {
    }
    public DbSet<Worker> TodoWorker { get; set; } = null!;

}
