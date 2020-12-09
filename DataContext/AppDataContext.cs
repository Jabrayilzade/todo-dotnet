using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoWish.Models;

namespace TodoWish.DataContext
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(x =>
            {
                x.ToTable("users");
                x.Property(s => s.Id).HasColumnName("id").IsRequired();
                x.Property(s => s.Email).HasColumnName("email").IsRequired();
                x.Property(s => s.Name).HasColumnName("name").IsRequired();
                x.Property(s => s.FirstName).HasColumnName("first_name").IsRequired();
                x.Property(s => s.UserRole).HasColumnName("user_role").IsRequired();
                x.Property(s => s.PasswordHash).HasColumnName("password_hash").IsRequired();
                x.Property(s => s.PasswordSalt).HasColumnName("password_salt").IsRequired();
                x.Property(s => s.RegisterDate).HasColumnName("register_date").IsRequired();
                x.Property(s => s.LastLog).HasColumnName("last_log").IsRequired();
            });

            modelBuilder.Entity<Todo>(x =>
            {
                x.ToTable("todos");
                x.Property(s => s.Id).HasColumnName("id").IsRequired();
                x.Property(s => s.Author).HasColumnName("author").IsRequired();
                x.Property(s => s.Content).HasColumnName("content").IsRequired();
                x.Property(s => s.Due).HasColumnName("due").IsRequired();
                x.Property(s => s.Status).HasColumnName("status").IsRequired();
                x.Property(s => s.UserId).HasColumnName("user_id").IsRequired();
            });

            modelBuilder.Entity<Project>(x =>
            {
                x.ToTable("projects");
                x.Property(s => s.Id).HasColumnName("id").IsRequired();
                x.Property(s => s.Title).HasColumnName("title").IsRequired();
                x.Property(s => s.Content).HasColumnName("content").IsRequired();
                x.Property(s => s.Due).HasColumnName("due").IsRequired();
                x.Property(s => s.UserId).HasColumnName("user_id").IsRequired();
            });
        }
    }
}