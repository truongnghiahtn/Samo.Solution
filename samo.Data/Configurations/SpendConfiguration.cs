using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using samo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Data.Configurations
{
    public class SpendConfiguration : IEntityTypeConfiguration<Spend>
    {
        public void Configure(EntityTypeBuilder<Spend> builder)
        {
            builder.ToTable("Spend");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Status).HasDefaultValue(true);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            
        }
    }
}
