using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SCA.BLLServices;
using SCA.BLLServices.Generic;
using SCA.DapperRepository;
using SCA.DapperRepository.Generic;
using SCA.DataAccess.Context;
using SCA.Entity.Entities;
using SCA.Entity.Model;
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

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

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

            services.AddMvc(config2 =>
            {
                // Add XML Content Negotiation
                config2.RespectBrowserAcceptHeader = true;
                config2.ReturnHttpNotAcceptable = true;
                // config2.InputFormatters.Add(new XmlSerializerInputFormatter());
                config2.OutputFormatters.Add(new XmlSerializerOutputFormatter());
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
            services.AddTransient<IAddressManager, AddressManager>();
            services.AddTransient<IDefinitionManager, DefinitionManager>();
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<ISocialMediaManager, SocialMediaManager>();
            services.AddTransient<ISender, SenderManager>();
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IReportManager, ReportManager>();
            services.AddTransient<IQuestionManager, QuestionManager>();
            services.AddTransient<IContentManager, ContentManager>();
            services.AddTransient<IPictureManager, PictureManager>();
            services.AddTransient<ITagManager, TagManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<IB2CManagerUI, B2CManagerUI>();
            services.AddTransient<IUserValidation, UserValidation>();
            services.AddTransient<ISyncManager, SyncManager>();
            services.AddTransient<IApiManager, ApiManager>();
            services.AddTransient<ICompanyClubManager, CompanyClubManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IErrorManagement, ErrorManagement>();
            services.AddTransient<INewsletterManager, NewsletterManager>();
            services.AddTransient<IPageManager, PageManager>();
            services.AddTransient<IMenuManager, MenuManager>();
            services.AddTransient<ICommentManager, CommentManager>();
            //services which implements generic service and repository
            services.AddTransient<IAnnounsmentService<Announsment>, AnnounsmentService>();
            services.AddTransient<IAnnounsment<Announsment>, AnnounsmentRepository>();
            services.AddTransient<IYoutubePlaylistService<YoutubePlaylist>, YoutubePlaylistService>();
            services.AddTransient<IYoutubePlaylist<YoutubePlaylist>, YoutubePlaylistRepository>();
            services.AddTransient<ICompanyClubService<SCA.Entity.Entities.CompanyClubs>, CompanyClubService>();
            services.AddTransient<ICompanyClub<SCA.Entity.Entities.CompanyClubs>, CompanyClubRepository>();
            services.AddTransient<ISocialMedia<SCA.Entity.Entities.SocialMedia>, SocialMediaRepository>();
            services.AddTransient<ISocialMediaService<SCA.Entity.Entities.SocialMedia>, SocialMediaService>();
            services.AddTransient<ICategory<SCA.Entity.Entities.Category>, CategoryRepository>();
            services.AddTransient<ICategoryService<SCA.Entity.Entities.Category>, CategoryService>();
            services.AddTransient<IContent<SCA.Entity.Entities.Content>, ContentRepository>();
            services.AddTransient<IContentService<SCA.Entity.Entities.Content>, ContentService>();
            services.AddTransient<IImageGalery<SCA.Entity.Entities.ImageGalery>, ImageGaleryRepository>();
            services.AddTransient<IImageGaleryService<SCA.Entity.Entities.ImageGalery>, ImageGaleryService>();

            //generic services
            services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<IHostedService, MailService>();
            #endregion

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddDistributedMemoryCache();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }

            app.UseDeveloperExceptionPage();

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
