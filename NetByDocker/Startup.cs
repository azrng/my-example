using AutoMapper;
using Common.EFCore;
using Common.Mvc.Exceptions.Middleware;
using Common.Mvc.Filter;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using NetByDocker.Domain;
using Newtonsoft.Json.Converters;
using System.Reflection;

namespace NetByDocker;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add Service
        services.AddControllers(options =>
        {
            //options.Filters.Add<CustomerActionFiter>();
            //options.ModelBinderProviders.Insert(0, new TokenModelBinderProvider());
            options.Filters.Add<CustomResultPackFilter>();
            options.Filters.Add<ModelVerifyFilter>();
        }).AddNewtonsoftJson(options =>
        {
            //时间格式化
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            //swagger显示枚举
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true; //是否启用默认的model校验
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebApi", Version = "v1" });
            // 使用反射获取xml文件。并构造出文件的路径
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
            c.IncludeXmlComments(xmlPath, true);
        });

        #region Add ORM

        var dbType = Configuration.GetValue<string>("DbConfig:EnabledDb");
        switch (dbType)
        {
            case "Mysql":
                //services.AddMySQLDb<OpenDbContext>(Configuration["DbConfig:Mysql:ConnectionString"]);
                break;

            case "Postgresssql":
                //services.AddPostgreSQLDb<OpenDbContext>(Configuration["DbConfig:Postgresssql:ConnectionString"]);
                break;

            default:
                services.AddEntityFramework<OpenDbContext>("DbConfig:Memory");
                break;
        }

        #endregion Add ORM

        //注入AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly().DefinedTypes.Where(t => typeof(Profile).GetTypeInfo()
            .IsAssignableFrom(t.AsType())).Select(t => t.AsType()).ToArray());

        //cors  收费的是否是
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", p =>
            {
                p.AllowAnyOrigin() //允许任务来源的主机访问
                    .AllowAnyMethod() //允许任何请求方式
                    .AllowAnyHeader(); //允许任何头部
            });
        });

        //IOC
        services.RegisterBusinessServices("NetByDocker.dll");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        //if (env.IsDevelopment())
        //{
        //}
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Net5ByDocker v1"));
        app.UseCustomExceptionMiddleware();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseCors("AllowAll");
        app.UseAuthorization();
    }
}