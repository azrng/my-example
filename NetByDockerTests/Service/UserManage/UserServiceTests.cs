using Coldairarrow.Util;
using Common.EFCore;
using Moq;
using NetByDocker.Domain;
using NetByDocker.Model.Enum;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NetByDocker.Service.UserManage.Tests
{
    public class UserServiceTests
    {
        [Fact()]
        public async void Get_User_ReutrnInfoTest()
        {
            //TODO 错误
            /*
             * System. InvalidOperationException : The provider for the source 'IQueryable' doesn't implement 'IAsyncQueryProvider'.
             * Only providers that implement 'IAsyncQueryProvider' can be used for Entity Framework asynchronous operations
             * */
            new IdHelperBootstrapper().SetWorkderId(1).Boot();

            //Arrange 赋值区域
            //模拟用户数据
            var user = new List<User>
            {
              new User("admin1", "123456", "张三", SexEnum.Man, 10.12d, 123456),
             };

            var userId = user.FirstOrDefault()?.Id ?? 0;
            // mock 数据
            var mockRepository = new Mock<IBaseRepository<User>>();
            mockRepository.Setup(t => t.EntitiesNoTacking).Returns(user.AsQueryable);
            var userService = new UserService(null, mockRepository.Object, null);

            //Act 行为阶段
            var result = await userService.GetDetailsAsync(userId, default);
            //Assert  断定阶段
            Assert.True(result.Id == userId);
        }
    }
}