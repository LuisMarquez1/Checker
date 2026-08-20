using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checker.Persistance.Configurations
{
    public class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
    {
        public void Configure(EntityTypeBuilder<Specification> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PartNumber)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
