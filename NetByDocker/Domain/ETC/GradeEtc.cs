using Common.EFCore.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetByDocker.Domain.ETC;

public class GradeEtc : EntityTypeConfigurationIdentityOperatorStatus<Group>
{
    public override void Configure(EntityTypeBuilder<Group> builder)
    {
        base.Configure(builder);

        builder.Property(t => t.Name).HasMaxLength(20).IsRequired().HasDefaultValue(string.Empty).HasComment("班级名称");
    }
}