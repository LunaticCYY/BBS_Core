using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BBS.Data;
using System;
using BBS.Services;
using BBS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BBS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BBSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BBSConnection")));

            services.AddDbContext<BBSContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<BBSContext>()
                .AddDefaultTokenProviders();

            //services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            services.AddSingleton<IOperation<User>, Operation<User>>();
            services.AddSingleton<IOperation<Node>, Operation<Node>>();
            services.AddSingleton<IOperation<NodeRecord>, Operation<NodeRecord>>();
            services.AddSingleton<IOperation<TopicRecord>, Operation<TopicRecord>>();
            services.AddSingleton<IOperation<FollowRecord>, Operation<FollowRecord>>();
            services.AddSingleton<ITopicOperation, TopicOperation>();
            services.AddSingleton<IReplyOperation, ReplyOperation>();
            services.AddSingleton<INodeRecordOperation, NodeRecordOperation>();
            services.AddSingleton<ITopicRecordOperation, TopicRecordOperation>();
            services.AddSingleton<IFollowRecordOperation, FollowRecordOperation>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<UserServices>();
            //services.AddMemoryCache();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Admin",
                    authBuilder =>
                    {
                        authBuilder.RequireClaim("Admin", "Allowed");
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller}/{action}",
                    defaults: new { action = "Index" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
