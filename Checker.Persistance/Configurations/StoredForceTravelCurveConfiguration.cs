using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checker.Persistance.Configurations
{
    public class StoredForceTravelCurveConfiguration : IEntityTypeConfiguration<StoredForceTravelCurve>
    {
        public void Configure(EntityTypeBuilder<StoredForceTravelCurve> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CurveJson)
                .HasColumnType("jsonb");
        }
    }
}
