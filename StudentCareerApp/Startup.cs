using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SCA.DataAccess.Context;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.DTO.ErrorDb;
using SCA.Entity.Model;
using SCA.Repository.UoW;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
             )
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateAudience = true,
                     ValidAudience = "ogrenciKariyeri",
                     ValidateIssuer = true,
                     ValidIssuer = "ogrenciKariyeri1",
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ogrenciKariyerikey"))
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnTokenValidated = ctx =>
                     {
                         //Gerekirse burada gelen token içerisindeki çeşitli bilgilere göre doğrulam yapılabilir.
                         return Task.CompletedTask;
                     },
                     OnAuthenticationFailed = ctx =>
                     {
                         Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                         return Task.CompletedTask;
                     }
                 };
             });

            #region Configurations
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            //});
            #endregion

            //    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.LoginPath = "/Admin/Login/Login/";
            //});

            #region Database

            services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>().BuildServiceProvider();

            #endregion

            #region Service Register
            services.AddTransient<IUnitofWork, UnitofWork>();
            services.AddTransient<IAddressManager, AddressManager>();
            services.AddTransient<IDefinitionManager, DefinitionManager>();
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<ISender, SenderManager>();
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IReportManager, ReportManager>();
            services.AddTransient<IQuestionManager, QuestionManager>();
            services.AddTransient<IContentManager, ContentManager>();
            services.AddTransient<IPictureManager, PictureManager>();
            services.AddTransient<ITagManager, TagManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<IMenuManager, MenuManager>();
            services.AddTransient<IAnalysisManager, AnalysisManager>();
            services.AddTransient<IB2CManagerUI, B2CManagerUI>();
            services.AddTransient<IUserValidation, UserValidation>();
            services.AddTransient<ISyncManager, SyncManager>();
            services.AddTransient<IApiManager, ApiManager>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IErrorManagement, ErrorManagement>();
            #endregion

            #region Auto Mapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                #region Address

                cfg.CreateMap<Cities, CitiesDto>().ReverseMap();
                cfg.CreateMap<District, DistrictDto>().ReverseMap();

                #endregion

                #region Education

                cfg.CreateMap<Department, DepartmentDto>().ReverseMap();
                cfg.CreateMap<EducationStatus, EducationStatusDto>().ReverseMap();
                cfg.CreateMap<Faculty, FacultyDto>().ReverseMap();
                cfg.CreateMap<HighSchool, HighSchoolDto>().ReverseMap();
                cfg.CreateMap<StudentClass, StudentClassDto>().ReverseMap();
                cfg.CreateMap<University, UniversityDto>().ReverseMap();

                #endregion

                #region Categories

                cfg.CreateMap<Category, MainCategoryDto>().ReverseMap();
                cfg.CreateMap<CategoryRelation, CategoryRelationDto>().ReverseMap();

                #endregion

                #region Users

                cfg.CreateMap<Users, UsersDTO>().ReverseMap();
                cfg.CreateMap<UserLog, UserLogDto>().ReverseMap();
                cfg.CreateMap<Users, UserShortInforDto>().ReverseMap();
                cfg.CreateMap<Users, UsersResultDTO>().ReverseMap();
                cfg.CreateMap<UsersDTO, UsersResultDTO>().ReverseMap();
                cfg.CreateMap<Users, UserMobilDto>().ReverseMap();
                cfg.CreateMap<Users, UserRegisterDto>().ReverseMap();
                cfg.CreateMap<UsersDTO, UserRegisterDto>().ReverseMap();
                cfg.CreateMap<UserSession, Users>().ReverseMap();

                #endregion

                #region Question

                cfg.CreateMap<Tests, TestsDto>().ReverseMap();
                cfg.CreateMap<TestValue, TestValueDto>().ReverseMap();
                cfg.CreateMap<Questions, QuestionsDto>().ReverseMap();
                cfg.CreateMap<QuestionOptions, QuestionOptionsDto>().ReverseMap();
                cfg.CreateMap<QuesitonAsnweByUsers, QuesitonAsnweByUsersDto>().ReverseMap();

                #endregion

                #region Read

                cfg.CreateMap<ReadCountOfTestAndContent, ReadCountOfTestAndContentDto>().ReverseMap();

                #endregion

                #region Content

                cfg.CreateMap<Content, ContentDto>().ReverseMap();
                cfg.CreateMap<Content, ContentShortListDto>().ReverseMap();
                cfg.CreateMap<Content, ContentShortListUIDto>().ReverseMap();
                cfg.CreateMap<Content, ContenUIDto>().ReverseMap();
                #endregion

                #region Image

                cfg.CreateMap<ImageModel, ReadCountOfTestAndContentDto>().ReverseMap();

                #endregion

                #region Tag

                cfg.CreateMap<Tags, TagDto>().ReverseMap();
                cfg.CreateMap<TagRelation, TagRelationDto>().ReverseMap();

                #endregion

                #region RoleType

                cfg.CreateMap<RoleType, RoleTypeDto>().ReverseMap();

                #endregion

                #region comment
                cfg.CreateMap<Comments, CommentsDto>().ReverseMap();
                #endregion

                #region Screen

                cfg.CreateMap<ScreenMaster, ScreenMasterDto>().ReverseMap();
                cfg.CreateMap<ScreenDetail, ScreenDetailDto>().ReverseMap();

                #endregion

                #region Screen

                cfg.CreateMap<SocialMedia, SocialMediaDto>().ReverseMap();

                #endregion

                #region Sector

                cfg.CreateMap<SectorDto, Sector>().ReverseMap();

                #endregion

                #region ScreenAnouncement

                cfg.CreateMap<AnouncementDto, Anouncement>().ReverseMap();

                #endregion

                #region ScreenAnouncement

                cfg.CreateMap<ImageGaleryDto, ImageGalery>().ReverseMap();

                #endregion

                #region Errors

                cfg.CreateMap<ErrorDto, Errors>().ReverseMap();

                #endregion

            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "/wwwroot";
                configuration.RootPath = "/wwwroot/AdminFiles/Template/assets/node_modules";
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddDistributedMemoryCache();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions() { FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")) });


            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "AdminFiles")),
            //    RequestPath = new PathString("/AdminFiles")
            //});

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
