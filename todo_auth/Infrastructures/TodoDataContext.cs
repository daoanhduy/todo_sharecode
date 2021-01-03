using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using todo_auth.Core.Entities;
using todo_auth.Infrastructures.Configurations;

namespace Infrastructures
{
    public class TodoDataContext : DbContext
    {
        public TodoDataContext(DbContextOptions<TodoDataContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskTodo> Tasks { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Role>().HasData(new Role { Id =1 ,Name = "member" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2 ,Name = "admin" });
        }
    }
}
