using Common.EFCore.Entities;

namespace NetByDocker.Domain;

/// <summary>
/// 成绩表
/// </summary>
public class Score : IdentityBaseEntity
{
    public Score()
    {
        CreateTime = DateTime.Now;
    }

    public Score(string courseName, int grade, long userId) : this()
    {
        CourseName = courseName;
        Grade = grade;
        UserId = userId;
    }

    /// <summary>
    /// 课程名称
    /// </summary>
    public string CourseName { get; set; }

    /// <summary>
    ///成绩
    /// </summary>
    public int Grade { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}