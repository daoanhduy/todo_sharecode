using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.Property(m => m.UserName).IsRequired(true).HasMaxLength(250);
            builder.Property(m => m.Password).IsRequired(true).HasMaxLength(250);
            builder.Property(m => m.FirstName).IsRequired(false);
            builder.Property(m => m.LastName).IsRequired(false);
            builder.Property(m => m.ExpirationDate).IsRequired(false);
            builder.HasIndex(m => m.UserName).IsUnique();
        }
    }
}
