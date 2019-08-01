using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SCA.DataAccess.Migrations
{
    public partial class second1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CityName = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PublishStateType = table.Column<byte>(nullable: false),
                    ReadCount = table.Column<long>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    SeoUrl = table.Column<string>(nullable: true),
                    Header = table.Column<string>(nullable: true),
                    Writer = table.Column<string>(nullable: true),
                    ConfirmUserId = table.Column<long>(nullable: false),
                    ConfirmUserName = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    ContentDescription = table.Column<string>(nullable: true),
                    IsHeadLine = table.Column<bool>(nullable: false),
                    IsManset = table.Column<bool>(nullable: false),
                    IsMainMenu = table.Column<bool>(nullable: false),
                    IsConstantMainMenu = table.Column<bool>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    InternId = table.Column<int>(nullable: false),
                    VisibleId = table.Column<int>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DepartmentCode = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    DepartmentName = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationStatus",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FacultyCode = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FacultyName = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageModel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ImageFolder = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Menus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScreenMaster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsSuperUser = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    SocialMediaType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentClass",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Hit = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Header = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    PublishData = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Readed = table.Column<int>(nullable: false),
                    PublishState = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryRelation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false),
                    TagContentId = table.Column<long>(nullable: false),
                    ReadType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryRelation_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CityId = table.Column<long>(nullable: false),
                    DistrictName = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_District_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HighSchool",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    HighSchoolCode = table.Column<long>(nullable: false),
                    SchoolName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighSchool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HighSchool_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UniversityCode = table.Column<long>(nullable: false),
                    UniversityName = table.Column<string>(maxLength: 200, nullable: true),
                    CityId = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.Id);
                    table.ForeignKey(
                        name: "FK_University_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RoleTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_RoleType_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ScreenMasterId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsSuperUser = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenDetail_ScreenMaster_ScreenMasterId",
                        column: x => x.ScreenMasterId,
                        principalTable: "ScreenMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagRelation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TagId = table.Column<long>(nullable: false),
                    TagsId = table.Column<long>(nullable: true),
                    TagContentId = table.Column<long>(nullable: false),
                    ReadType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagRelation_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TestId = table.Column<long>(nullable: false),
                    TestsId = table.Column<long>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestValue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TestId = table.Column<long>(nullable: false),
                    TestsId = table.Column<long>(nullable: true),
                    FirstValue = table.Column<int>(nullable: false),
                    SecondValue = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestValue_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    RoleTypeId = table.Column<long>(nullable: false),
                    RoleExpiresDate = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<byte>(nullable: false),
                    EducationStatusId = table.Column<long>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    HighSchoolTypeId = table.Column<long>(nullable: true),
                    UniversityId = table.Column<long>(nullable: true),
                    FacultyId = table.Column<long>(nullable: true),
                    DepartmentId = table.Column<long>(nullable: true),
                    ClassId = table.Column<long>(nullable: true),
                    ClassTypeId = table.Column<long>(nullable: true),
                    IsStudent = table.Column<bool>(nullable: false),
                    Biography = table.Column<string>(nullable: true),
                    CityId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ReferanceCode = table.Column<string>(nullable: true),
                    EnrollPlatformTypeId = table.Column<int>(nullable: false),
                    DistrictId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_StudentClass_ClassTypeId",
                        column: x => x.ClassTypeId,
                        principalTable: "StudentClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_EducationStatus_EducationStatusId",
                        column: x => x.EducationStatusId,
                        principalTable: "EducationStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_HighSchool_HighSchoolTypeId",
                        column: x => x.HighSchoolTypeId,
                        principalTable: "HighSchool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_RoleType_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "University",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<long>(nullable: false),
                    QuestionsId = table.Column<long>(nullable: true),
                    CheckOption = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    FreeText = table.Column<string>(nullable: true),
                    Answer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReadType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ArticleId = table.Column<long>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    UserID = table.Column<long>(nullable: false),
                    UsersId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuesitonAsnweByUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UsersId = table.Column<long>(nullable: true),
                    TestId = table.Column<long>(nullable: false),
                    TestsId = table.Column<long>(nullable: true),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuesitonAsnweByUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuesitonAsnweByUsers_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuesitonAsnweByUsers_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReadCountOfTestAndContent",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TestId = table.Column<long>(nullable: false),
                    TestsId = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    UsersId = table.Column<long>(nullable: true),
                    ReadType = table.Column<int>(nullable: false),
                    ReadId = table.Column<long>(nullable: false),
                    PlatformType = table.Column<int>(nullable: false),
                    Count = table.Column<long>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadCountOfTestAndContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadCountOfTestAndContent_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReadCountOfTestAndContent_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<long>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<long>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UsersId = table.Column<long>(nullable: true),
                    PlatformTypeId = table.Column<int>(nullable: false),
                    EnteraceDate = table.Column<DateTime>(nullable: false),
                    IpAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLog_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRelation_CategoryId",
                table: "CategoryRelation",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UsersId",
                table: "Comments",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_District_CityId",
                table: "District",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_HighSchool_CityId",
                table: "HighSchool",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_QuesitonAsnweByUsers_TestsId",
                table: "QuesitonAsnweByUsers",
                column: "TestsId");

            migrationBuilder.CreateIndex(
                name: "IX_QuesitonAsnweByUsers_UsersId",
                table: "QuesitonAsnweByUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionsId",
                table: "QuestionOptions",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestsId",
                table: "Questions",
                column: "TestsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadCountOfTestAndContent_TestsId",
                table: "ReadCountOfTestAndContent",
                column: "TestsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadCountOfTestAndContent_UsersId",
                table: "ReadCountOfTestAndContent",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleTypeId",
                table: "RolePermission",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenDetail_ScreenMasterId",
                table: "ScreenDetail",
                column: "ScreenMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TagRelation_TagsId",
                table: "TagRelation",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_TestValue_TestsId",
                table: "TestValue",
                column: "TestsId");

            migrationBuilder.CreateIndex(
                name: "IX_University_CityId",
                table: "University",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLog_UsersId",
                table: "UserLog",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassTypeId",
                table: "Users",
                column: "ClassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DistrictId",
                table: "Users",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EducationStatusId",
                table: "Users",
                column: "EducationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FacultyId",
                table: "Users",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HighSchoolTypeId",
                table: "Users",
                column: "HighSchoolTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleTypeId",
                table: "Users",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UniversityId",
                table: "Users",
                column: "UniversityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRelation");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "ImageModel");

            migrationBuilder.DropTable(
                name: "QuesitonAsnweByUsers");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "ReadCountOfTestAndContent");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "ScreenDetail");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "TagRelation");

            migrationBuilder.DropTable(
                name: "TestValue");

            migrationBuilder.DropTable(
                name: "UserLog");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ScreenMaster");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "StudentClass");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "EducationStatus");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropTable(
                name: "HighSchool");

            migrationBuilder.DropTable(
                name: "RoleType");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
