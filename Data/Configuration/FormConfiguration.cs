using FormMaker.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Data.Configuration
{
    public class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.HasKey(q => q.FormID);
            builder.Property(q => q.FormID).ValueGeneratedOnAdd().IsRequired();

            builder.Property(f => f.FormTitle).IsRequired().HasMaxLength(200);
            builder.Property(f => f.FormDescription).HasMaxLength(500);

            builder.HasMany(f => f.FormQuestions).WithOne(fq => fq.Form).HasForeignKey(fq => fq.FormID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(f => f.FormProcesses).WithOne(fp => fp.Form).HasForeignKey(fp => fp.FormID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
