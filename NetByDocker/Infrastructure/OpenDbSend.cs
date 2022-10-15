using Microsoft.EntityFrameworkCore;
using NetByDocker.Domain;

namespace NetByDocker.Infrastructure;

public class OpenDbSend
{
    /// <summary>
    /// 生成数据库以及数据种子
    /// </summary>
    /// <param name="dbContext">数据库上下文</param>
    /// <param name="loggerFactory">日志</param>
    /// <param name="retry">重试次数</param>
    /// <returns></returns>
    public static async Task SeedAsync(OpenDbContext dbContext,
        ILoggerFactory loggerFactory,
        int? retry = 0)
    {
        var retryForAvailability = retry.Value;
        try
        {
            dbContext.Database.Migrate(); //如果当前数据库不存在按照当前 model 创建，如果存在则将数据库调整到和当前 model 匹配
            await InitializeAsync(dbContext).ConfigureAwait(false);

            //if (dbContext.Database.EnsureCreated())//如果当前数据库不存在按照当前 model创建，如果存在则不管了。
            //  await InitializeAsync(dbContext).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (retryForAvailability < 3)
            {
                retryForAvailability++;
                var log = loggerFactory.CreateLogger<OpenDbSend>();
                log.LogError(ex.Message);
                await SeedAsync(dbContext, loggerFactory, retryForAvailability).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private static async Task InitializeAsync(OpenDbContext context)
    {
        if (!context.Set<User>().Any())
        {
            //var user1 = new User("张三", "123456", SexEnum.Man);
            //await context.Set<User>().AddAsync(user1).ConfigureAwait(false);
            //var scoreList1 = new List<Score> {
            //    new Score("语文", 80,user1.Id),
            //    new Score("数学", 90,user1.Id)
            //};
            //await context.Set<Score>().AddRangeAsync(scoreList1);

            //var user2 = new User("李四", "123456", SexEnum.WoMan);
            //await context.Set<User>().AddAsync(user2).ConfigureAwait(false);
            //var scoreList2 = new List<Score> {
            //    new Score("语文", 70,user2.Id),
            //    new Score("数学", 60,user2.Id)
            //};
            //await context.Set<Score>().AddRangeAsync(scoreList2);

            //await context.Set<User>().AddAsync(new User("王五", "123456", SexEnum.Man)).ConfigureAwait(false);
        }

        await context.SaveChangesAsync().ConfigureAwait(false);
    }
}