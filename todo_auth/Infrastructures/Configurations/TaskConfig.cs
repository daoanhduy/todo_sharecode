using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseLayer.Configurations
{
    public class TaskConfig : IEntityTypeConfiguration<TaskTodo>
    {
        public void Configure(EntityTypeBuilder<TaskTodo> builder)
        {
            builder.ToTable(nameof(TaskTodo));
            builder.Property(m => m.Title).IsRequired(true);
            builder.Property(m => m.End_At).IsRequired(true);
        }
    }
}
