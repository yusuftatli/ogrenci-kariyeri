using Microsoft.EntityFrameworkCore;
using SCA.Entity.Model;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DataAccess.Context
{
    public class PostgreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
               => optionsBuilder.UseNpgsql("Host=localhost;Database=StudentDb;Username=postgres;Password=yt@12345;Port=5432");


        #region Address
        public DbSet<Cities> Cities { get; set; }
        public DbSet<District> District { get; set; }
        #endregion

        #region Universities
        public DbSet<Department> Department { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<University> University { get; set; }
        #endregion

        #region Users
        public DbSet<Users> Users { get; set; }
        #endregion

        #region Categories
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryRelation> CategoryRelation { get; set; }
        
        #endregion

        #region Roles
        public DbSet<RoleType> RoleType { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        #endregion

        #region Content
        public DbSet<Content> Content { get; set; }
        #endregion

        #region Image
        public DbSet<ImageModel> ImageModel { get; set; }
        #endregion

        #region Tag
        public DbSet<Tags> Tags { get; set; }
        public DbSet<TagRelation> TagRelation { get; set; }
        #endregion

        #region Menus
        public DbSet<MenuList> MenuList { get; set; }
        public DbSet<MenuRelationWithRole> MenuRelationWithUser { get; set; }
        #endregion

    }
}
