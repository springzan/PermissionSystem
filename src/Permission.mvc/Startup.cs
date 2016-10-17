﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Permission.efcore;
using Microsoft.EntityFrameworkCore;
using Permission.domain.IRepositories;
using Permission.efcore.Repositories;
using Permission.application.UserApp;

namespace Permission.mvc
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",optional:true,reloadOnChange:true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //获取数据库连接字符串
            var sqlConnectString = Configuration.GetConnectionString("DefaultConnection");
            //添加数据上下文
            services.AddDbContext<PermissionDbContext>(options=>options.UseNpgsql(sqlConnectString));
            //仓储及服务进行依赖注入
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();

            services.AddMvc();

            //Session服务
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                //开发环境异常处理
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //生产环境异常处理
                app.UseExceptionHandler("/Shared/Error");
            }
            //使用静态文件
            app.UseStaticFiles();
            //请求管道中启用Session
            app.UseSession();
            //使用Mvc，设置默认路由为系统登录
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });

            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
