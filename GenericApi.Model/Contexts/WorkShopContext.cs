using GenericApi.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Model.Contexts
{
    public class WorkShopContext : BaseDbContext
    {
        public WorkShopContext(DbContextOptions<WorkShopContext> options) : base(options)
        {
        }
        public DbSet<Document> Documents { get; set; }
        public DbSet<WorkShop> WorkShops { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkShopMember> WorkShopMembers { get; set; }
        public DbSet<WorkShopDay> WorkShopDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
             new User {
                CreatedDate = DateTimeOffset.UtcNow,
                Dob = DateTime.Now,
                Gender = Core.Enums.Gender.MALE,
                LastName = "Admin",
                MiddleName = "Admin",
                Name ="Admin",
                Password = "Hola123,",
                SecondLastName = "Admin",
                UserName = "Admin",
                DocumentType = Core.Enums.DocumentType.ID,
                DocumentTypeValue = "00000000000",
                Id = 1
             });

            base.OnModelCreating(modelBuilder);
        }
    }
}
