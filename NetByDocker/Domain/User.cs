using Common.EFCore.Entities;
using NetByDocker.Model.Enum;

namespace NetByDocker.Domain;

/// <summary>
/// 用户信息
/// </summary>
public class User : IdentityOperatorStatusEntity
{
    public User(string account, string passWord, string name, SexEnum sex, double credit, long groupId)
    {
        Account = account;
        PassWord = passWord;
        Name = name;
        Sex = sex;
        Credit = credit;
        GroupId = groupId;
    }

    /// <summary>
    /// 账号
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string PassWord { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public SexEnum Sex { get; set; }

    /// <summary>
    /// 学分
    /// </summary>
    public double Credit { get; set; }

    /// <summary>
    /// 班级ID
    /// </summary>
    public long GroupId { get; set; }
}