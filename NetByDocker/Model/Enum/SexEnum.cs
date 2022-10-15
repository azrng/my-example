using System.ComponentModel;

namespace NetByDocker.Model.Enum;

/// <summary>
/// 性别枚举  0未知 1男  2女
/// </summary>
public enum SexEnum
{
    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    Noknow = 0,

    /// <summary>
    /// 男
    /// </summary>
    [Description("男")]
    Man = 1,

    /// <summary>
    /// 女
    /// </summary>
    [Description("女")]
    WoMan = 2
}
