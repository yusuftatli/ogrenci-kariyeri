using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SCA.DataAccess.Context;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Repository.UoW;
using SCA.Services;
using SCA.Services.Interface;
using SCA.Services.Interface.InterfaceUI;

namespace StudentCareerApp
{
    public class Startup
    {
        public IConfiguration configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            #region Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            #endregion

            #region Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44309/",
                    ValidAudience = "https://localhost:44309/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });
            #endregion

            #region Configurations
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            });
            #endregion

            #region Database
            services.AddEntityFrameworkNpgsql()
            .AddDbContext<PostgreDbContext>()
            .BuildServiceProvider();
            #endregion

            #region Service Register
            services.AddTransient<IUnitofWork, UnitofWork>();
            services.AddTransient<IAddressManager, AddressManager>();
            services.AddTransient<IEducationManager, EducationManager>();
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
                cfg.CreateMap<UserCreateAnlitic, UserCreateAnliticDto>().ReverseMap();

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
                cfg.CreateMap<Content, ContentForHomePageDTO>().ReverseMap();
                cfg.CreateMap<Content, ContentDetailForDetailPageDTO>().ReverseMap();
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


                #region Menun

                cfg.CreateMap<MenuList, MenuListDto>().ReverseMap();
                cfg.CreateMap<MenuRelationWithRole, MenuRelationWithRoleDto>().ReverseMap();

                #endregion

            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Unauthorized",
                            Msg = "token expired"
                        }));
                    }
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Internal State Error",
                            Msg = error.Error.Message
                        }));
                    }
                    else
                        await next();
                });
            });

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "AdminFiles")),
                RequestPath = new PathString("/AdminFiles")
            });


            app.UseSession();

            app.UseCors("AllowAll");

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
