using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FormMaker.Model;

namespace FormMaker.Data.Configuration
{
    public class ProcessConfiguration : IEntityTypeConfiguration<Process>
    {
        public void Configure(EntityTypeBuilder<Process> builder)
        {
            builder.HasKey(q => q.ProcessID);
            builder.Property(q => q.ProcessID).ValueGeneratedOnAdd().IsRequired();

            builder.Property(p => p.ProcessTitle).IsRequired().HasMaxLength(200);
            builder.Property(p => p.ProcessDescription).HasMaxLength(500);

            builder.HasMany(p => p.FormProcesses).WithOne(fp => fp.Process).HasForeignKey(fp => fp.ProcessID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
