using Common.EFCore.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetByDocker.Domain.ETC;

public class UserEtc : EntityTypeConfigurationIdentityOperatorStatus<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Account).IsRequired().HasMaxLength(20).HasComment("账号标识");
        builder.Property(x => x.PassWord).IsRequired().HasMaxLength(20).HasComment("密码");
        builder.Property(x => x.CreateTime).IsRequired().HasComment("创建时间");
        builder.Property(x => x.Sex).IsRequired().HasComment("性别");
    }
}