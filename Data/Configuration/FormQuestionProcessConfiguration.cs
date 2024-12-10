using FormMaker.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Data.Configuration
{
    public class FormQuestionProcessConfiguration : IEntityTypeConfiguration<FormQuestionProcess>
    {
        public void Configure(EntityTypeBuilder<FormQuestionProcess> builder)
        {
            builder.HasKey(q => q.FormQuestionProcessID);
            builder.Property(q => q.FormQuestionProcessID).ValueGeneratedOnAdd().IsRequired();

            //builder.HasOne(fqp => fqp.FormQuestion).WithMany().HasForeignKey(fqp => fqp.FormQuestionID).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(fqp => fqp.FormProcess).WithMany().HasForeignKey(fqp => fqp.FormProcessID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(fqp => fqp.Answers).WithOne(a => a.FormQuestionProcess).HasForeignKey(a => a.FormQuestionProcessID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

