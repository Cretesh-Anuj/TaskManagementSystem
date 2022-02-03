using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Text;
using TaskManagementSystem.Models;


namespace TaskManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("MyUsers");
            });
            

            modelBuilder.Entity<UsersTask>()
             .HasKey(bc => new { bc.TaskId, bc.ApplicationUserId });

            modelBuilder.Entity<UsersTask>()
                .HasOne(bc => bc.Task)
                .WithMany(b => b.UserTasks)
                .HasForeignKey(bc => bc.TaskId);

            modelBuilder.Entity<UsersTask>()
                .HasOne(bc => bc.ApplicationUser)
                .WithMany(c => c.UserTasks)
                .HasForeignKey(bc => bc.ApplicationUserId);
        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<UsersTask> UsersTasks { get; set; }
        
       
    }
}
