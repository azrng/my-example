using Common.EFCore.Entities;

namespace NetByDocker.Domain;

/// <summary>
/// 班级表
/// </summary>
public class Group : IdentityOperatorStatusEntity
{
    public Group()
    {
    }

    public Group(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 班级名称
    /// </summary>
    public string Name { get; set; }
}