﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using samo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Data.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRoles");

            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();

        }
    }
}
