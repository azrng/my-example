using NetByDocker;

/*
项目说明
1.简单的操作数据库，连接数据库，docker部署
2.使用EFCore + linq进行表关联操作数据库等
 */

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration, builder.Environment);

//注入服务
startup.ConfigureServices(builder.Services);

var app = builder.Build();

//使用管道
startup.Configure(app, app.Environment);

app.MapControllers();

app.Run();
//app.Run("http://*:8080");//设置自定义端口