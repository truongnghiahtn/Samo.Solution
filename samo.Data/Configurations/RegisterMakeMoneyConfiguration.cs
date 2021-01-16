using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using samo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Data.Configurations
{
    class RegisterMakeMoneyConfiguration : IEntityTypeConfiguration<RegisterMakeMoney>
    {
        public void Configure(EntityTypeBuilder<RegisterMakeMoney> builder)
        {
            builder.ToTable("RegisterMakeMoney");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Money).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.AppUser).WithMany(x => x.RegisterMakeMoneys).HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.makeMoney).WithMany(x => x.RegisterMakeMoneys).HasForeignKey(x => x.IdMakeMoney);
        }
    }
}
