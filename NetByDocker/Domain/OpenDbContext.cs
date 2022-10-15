using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace NetByDocker.Domain;

public class OpenDbContext : DbContext
{
    public OpenDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Group> Grades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //另一种配置连接数据库的方式
        //optionsBuilder.UseMySql("连接数据库", ServerVersion.AutoDetect("连接数据库字符串"));

        //显示敏感数据日志
        optionsBuilder.EnableSensitiveDataLogging(true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //属性配置
        //modelBuilder.Entity<User>().Property(t => t.Account).IsRequired().HasMaxLength(20).HasComment("帐号");
        //种子数据设置
        //modelBuilder.Entity<User>().HasData(new User { Account="种子"});

        // 添加etc
        //modelBuilder.ApplyConfiguration(new UserInfoETC());

        //使用下面的方法进行替换处理上面批量增加etc的操作
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}