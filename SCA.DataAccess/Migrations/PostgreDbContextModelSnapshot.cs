﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SCA.DataAccess.Context;

namespace SCA.DataAccess.Migrations
{
    [DbContext(typeof(PostgreDbContext))]
    partial class PostgreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SCA.Entity.Model.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<long?>("ParentId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("SCA.Entity.Model.CategoryRelation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CategoryId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("ReadType");

                    b.Property<long>("TagContentId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryRelation");
                });

            modelBuilder.Entity("SCA.Entity.Model.Cities", b =>
                {
                    b.Property<long>("CityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityName")
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("SCA.Entity.Model.Comments", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<long>("ArticleId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("ReadType");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.Property<long>("UserID");

                    b.Property<long?>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SCA.Entity.Model.Content", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<long>("ConfirmUserId");

                    b.Property<string>("ConfirmUserName");

                    b.Property<string>("ContentDescription");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<int>("EventId");

                    b.Property<string>("Header");

                    b.Property<string>("ImagePath");

                    b.Property<int>("InternId");

                    b.Property<bool>("IsConstantMainMenu");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsHeadLine");

                    b.Property<bool>("IsMainMenu");

                    b.Property<bool>("IsManset");

                    b.Property<DateTime>("PublishDate");

                    b.Property<byte>("PublishStateType");

                    b.Property<long>("ReadCount");

                    b.Property<string>("SeoUrl");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.Property<int>("VisibleId");

                    b.Property<string>("Writer");

                    b.HasKey("Id");

                    b.ToTable("Content");
                });

            modelBuilder.Entity("SCA.Entity.Model.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<long>("DepartmentCode");

                    b.Property<string>("DepartmentName")
                        .HasMaxLength(200);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("SCA.Entity.Model.District", b =>
                {
                    b.Property<long>("DistrictId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CityId");

                    b.Property<string>("DistrictName")
                        .HasMaxLength(80);

                    b.HasKey("DistrictId");

                    b.HasIndex("CityId");

                    b.ToTable("District");
                });

            modelBuilder.Entity("SCA.Entity.Model.EducationStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.ToTable("EducationStatus");
                });

            modelBuilder.Entity("SCA.Entity.Model.Faculty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<long>("FacultyCode");

                    b.Property<string>("FacultyName")
                        .HasMaxLength(200);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("SCA.Entity.Model.HighSchool", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CityId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<long>("HighSchoolCode");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("SchoolName");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("HighSchool");
                });

            modelBuilder.Entity("SCA.Entity.Model.ImageModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("ImageFolder");

                    b.Property<string>("ImageName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.ToTable("ImageModel");
                });

            modelBuilder.Entity("SCA.Entity.Model.MenuList", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<string>("Icon");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<long?>("ParentId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("MenuList");
                });

            modelBuilder.Entity("SCA.Entity.Model.MenuRelationWithRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("MenuId");

                    b.Property<long?>("MenuListId");

                    b.Property<long>("RoleId");

                    b.Property<long?>("RoleTypeId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.Property<long?>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("MenuListId");

                    b.HasIndex("RoleTypeId");

                    b.HasIndex("UsersId");

                    b.ToTable("MenuRelationWithUser");
                });

            modelBuilder.Entity("SCA.Entity.Model.QuesitonAsnweByUsers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Score");

                    b.Property<long>("TestId");

                    b.Property<long?>("TestsId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.Property<long>("UserId");

                    b.Property<long?>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("TestsId");

                    b.HasIndex("UsersId");

                    b.ToTable("QuesitonAsnweByUsers");
                });

            modelBuilder.Entity("SCA.Entity.Model.QuestionOptions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Answer");

                    b.Property<bool>("CheckOption");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<string>("FreeText");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("QuestionId");

                    b.Property<long?>("QuestionsId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionsId");

                    b.ToTable("QuestionOptions");
                });

            modelBuilder.Entity("SCA.Entity.Model.Questions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("TestId");

                    b.Property<long?>("TestsId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.HasIndex("TestsId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SCA.Entity.Model.ReadCountOfTestAndContent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Count");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<DateTime>("InsertDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("PlatformType");

                    b.Property<long>("ReadId");

                    b.Property<int>("ReadType");

                    b.Property<long>("TestId");

                    b.Property<long?>("TestsId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.Property<long>("UserId");

                    b.Property<long?>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("TestsId");

                    b.HasIndex("UsersId");

                    b.ToTable("ReadCountOfTestAndContent");
                });

            modelBuilder.Entity("SCA.Entity.Model.RolePermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("RoleTypeId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleTypeId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("SCA.Entity.Model.RoleType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.ToTable("RoleType");
                });

            modelBuilder.Entity("SCA.Entity.Model.StudentClass", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.ToTable("StudentClass");
                });

            modelBuilder.Entity("SCA.Entity.Model.TagRelation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("ReadType");

                    b.Property<long>("TagContentId");

                    b.Property<long>("TagId");

                    b.Property<long?>("TagsId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.HasIndex("TagsId");

                    b.ToTable("TagRelation");
                });

            modelBuilder.Entity("SCA.Entity.Model.Tags", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<long>("Hit");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SCA.Entity.Model.TestValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Description");

                    b.Property<int>("FirstValue");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("SecondValue");

                    b.Property<long>("TestId");

                    b.Property<long?>("TestsId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.HasIndex("TestsId");

                    b.ToTable("TestValue");
                });

            modelBuilder.Entity("SCA.Entity.Model.Tests", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<string>("Header");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Label");

                    b.Property<string>("PublishData");

                    b.Property<byte>("PublishState");

                    b.Property<int>("Readed");

                    b.Property<string>("Topic");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("SCA.Entity.Model.University", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CityId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("UniversityCode");

                    b.Property<string>("UniversityName")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("University");
                });

            modelBuilder.Entity("SCA.Entity.Model.UserLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<DateTime>("EnteraceDate");

                    b.Property<string>("IpAddress");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("PlatformTypeId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.Property<long>("UserId");

                    b.Property<long?>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("UserLog");
                });

            modelBuilder.Entity("SCA.Entity.Model.Users", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Category");

                    b.Property<long?>("CityId");

                    b.Property<long?>("ClassId");

                    b.Property<long?>("ClassTypeId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CreatedUserId");

                    b.Property<DateTime>("DeletedDate");

                    b.Property<long>("DeletedUserId");

                    b.Property<long?>("DepartmentId");

                    b.Property<long?>("DistrictId");

                    b.Property<long>("EducationStatusId");

                    b.Property<string>("EmailAddress");

                    b.Property<int>("EnrollPlatformTypeId");

                    b.Property<long?>("FacultyId");

                    b.Property<byte>("GenderId");

                    b.Property<long?>("HighSchoolTypeId");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("ReferanceCode");

                    b.Property<DateTime>("RoleExpiresDate");

                    b.Property<long>("RoleTypeId");

                    b.Property<string>("Surname");

                    b.Property<long?>("UniversityId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<long>("UpdatedUserId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("ClassTypeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("EducationStatusId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("HighSchoolTypeId");

                    b.HasIndex("RoleTypeId");

                    b.HasIndex("UniversityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SCA.Entity.Model.CategoryRelation", b =>
                {
                    b.HasOne("SCA.Entity.Model.Category", "Category")
                        .WithMany("CategoryRelation")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SCA.Entity.Model.Comments", b =>
                {
                    b.HasOne("SCA.Entity.Model.Users", "Users")
                        .WithMany("Comments")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("SCA.Entity.Model.District", b =>
                {
                    b.HasOne("SCA.Entity.Model.Cities", "Cities")
                        .WithMany("District")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SCA.Entity.Model.HighSchool", b =>
                {
                    b.HasOne("SCA.Entity.Model.Cities", "Cities")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SCA.Entity.Model.MenuRelationWithRole", b =>
                {
                    b.HasOne("SCA.Entity.Model.MenuList", "MenuList")
                        .WithMany("MenuRelationWithUser")
                        .HasForeignKey("MenuListId");

                    b.HasOne("SCA.Entity.Model.RoleType", "RoleType")
                        .WithMany("MenuRelationWithRole")
                        .HasForeignKey("RoleTypeId");

                    b.HasOne("SCA.Entity.Model.Users")
                        .WithMany("MenuRelationWithUser")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("SCA.Entity.Model.QuesitonAsnweByUsers", b =>
                {
                    b.HasOne("SCA.Entity.Model.Tests", "Tests")
                        .WithMany()
                        .HasForeignKey("TestsId");

                    b.HasOne("SCA.Entity.Model.Users", "Users")
                        .WithMany("QuesitonAsnweByUsers")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("SCA.Entity.Model.QuestionOptions", b =>
                {
                    b.HasOne("SCA.Entity.Model.Questions", "Questions")
                        .WithMany("QuestionOptions")
                        .HasForeignKey("QuestionsId");
                });

            modelBuilder.Entity("SCA.Entity.Model.Questions", b =>
                {
                    b.HasOne("SCA.Entity.Model.Tests", "Tests")
                        .WithMany("Questions")
                        .HasForeignKey("TestsId");
                });

            modelBuilder.Entity("SCA.Entity.Model.ReadCountOfTestAndContent", b =>
                {
                    b.HasOne("SCA.Entity.Model.Tests", "Tests")
                        .WithMany("ReadCountOfTestAndContent")
                        .HasForeignKey("TestsId");

                    b.HasOne("SCA.Entity.Model.Users", "Users")
                        .WithMany("ReadCountOfTestAndContent")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("SCA.Entity.Model.RolePermission", b =>
                {
                    b.HasOne("SCA.Entity.Model.RoleType", "RoleType")
                        .WithMany("RolePermission")
                        .HasForeignKey("RoleTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SCA.Entity.Model.TagRelation", b =>
                {
                    b.HasOne("SCA.Entity.Model.Tags", "Tags")
                        .WithMany("TagRelation")
                        .HasForeignKey("TagsId");
                });

            modelBuilder.Entity("SCA.Entity.Model.TestValue", b =>
                {
                    b.HasOne("SCA.Entity.Model.Tests", "Tests")
                        .WithMany("TestValue")
                        .HasForeignKey("TestsId");
                });

            modelBuilder.Entity("SCA.Entity.Model.University", b =>
                {
                    b.HasOne("SCA.Entity.Model.Cities", "Cities")
                        .WithMany("University")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SCA.Entity.Model.UserLog", b =>
                {
                    b.HasOne("SCA.Entity.Model.Users", "Users")
                        .WithMany("UserLog")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("SCA.Entity.Model.Users", b =>
                {
                    b.HasOne("SCA.Entity.Model.Cities", "Cities")
                        .WithMany("Users")
                        .HasForeignKey("CityId");

                    b.HasOne("SCA.Entity.Model.StudentClass", "ClassType")
                        .WithMany("Users")
                        .HasForeignKey("ClassTypeId");

                    b.HasOne("SCA.Entity.Model.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("SCA.Entity.Model.District")
                        .WithMany("Users")
                        .HasForeignKey("DistrictId");

                    b.HasOne("SCA.Entity.Model.EducationStatus", "EducationStatus")
                        .WithMany("Users")
                        .HasForeignKey("EducationStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SCA.Entity.Model.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId");

                    b.HasOne("SCA.Entity.Model.HighSchool", "HighSchoolType")
                        .WithMany("Users")
                        .HasForeignKey("HighSchoolTypeId");

                    b.HasOne("SCA.Entity.Model.RoleType", "RoleType")
                        .WithMany("Users")
                        .HasForeignKey("RoleTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SCA.Entity.Model.University", "University")
                        .WithMany()
                        .HasForeignKey("UniversityId");
                });
#pragma warning restore 612, 618
        }
    }
}
