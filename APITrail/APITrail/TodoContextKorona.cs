using System;

using Microsoft.EntityFrameworkCore;


namespace TodoApi.Models;


public class TodoContextKorona : DbContext
{
    public TodoContextKorona(DbContextOptions<TodoContextKorona> options)
        : base(options)
    {
    }
    public DbSet<Korona> TodoKorona { get; set; } = null!;

}
