using Common.EFCore.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetByDocker.Domain.ETC;

public class ScoreEtc : EntityTypeConfigurationIdentity<Score>
{
    public override void Configure(EntityTypeBuilder<Score> builder)
    {
        builder.Property(t => t.CourseName).HasMaxLength(20).IsRequired().HasDefaultValue(string.Empty)
            .HasComment("课程名");
        builder.Property(t => t.UserId).IsRequired().HasComment("用户ID");
        builder.Property(t => t.Grade).IsRequired().HasDefaultValue(0).HasComment("成绩");
        builder.Property(t => t.CreateTime).IsRequired().HasComment("创建时间");
        base.Configure(builder);
    }
}