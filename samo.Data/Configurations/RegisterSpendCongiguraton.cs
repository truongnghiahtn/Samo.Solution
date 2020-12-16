using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using samo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Data.Configurations
{
    class RegisterSpendCongiguraton : IEntityTypeConfiguration<RegisterSpend>
    {
        public void Configure(EntityTypeBuilder<RegisterSpend> builder)
        {
            builder.ToTable("RegisterSpend");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Money).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x=>x.DateCreate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.HasOne(x => x.AppUser).WithMany(x => x.RegisterSpends).HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.Spend).WithMany(x => x.RegisterSpends).HasForeignKey(x => x.IdSpend);
        }
    }
}
