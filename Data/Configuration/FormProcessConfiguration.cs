using FormMaker.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Data.Configuration
{
    public class FormProcessConfiguration : IEntityTypeConfiguration<FormProcess>
    {
        public void Configure(EntityTypeBuilder<FormProcess> builder)
        {
            builder.HasKey(q => q.FormProcessID);
            builder.Property(q => q.FormProcessID).ValueGeneratedOnAdd().IsRequired();

            builder.Property(fp => fp.Stage).IsRequired();

            //builder.HasOne(fp => fp.Form).WithMany(f => f.FormProcesses).HasForeignKey(fp => fp.FormID).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(fp => fp.Process).WithMany(p => p.FormProcesses).HasForeignKey(fp => fp.ProcessID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(fp => fp.FormQuestionProcesses).WithOne(fqp => fqp.FormProcess).HasForeignKey(fqp => fqp.FormProcessID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
