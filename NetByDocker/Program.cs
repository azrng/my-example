using NetByDocker;

/*
��Ŀ˵��
1.�򵥵Ĳ������ݿ⣬�������ݿ⣬docker����
2.ʹ��EFCore + linq���б�����������ݿ��
 */

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration, builder.Environment);

//ע�����
startup.ConfigureServices(builder.Services);

var app = builder.Build();

//ʹ�ùܵ�
startup.Configure(app, app.Environment);

app.MapControllers();

app.Run();
//app.Run("http://*:8080");//�����Զ���˿�