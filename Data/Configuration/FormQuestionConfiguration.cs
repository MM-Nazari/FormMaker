using FormMaker.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Data.Configuration
{
    public class FormQuestionConfiguration : IEntityTypeConfiguration<FormQuestion>
    {
        public void Configure(EntityTypeBuilder<FormQuestion> builder)
        {
            builder.HasKey(q => q.FormQuestionID);
            builder.Property(q => q.FormQuestionID).ValueGeneratedOnAdd().IsRequired();

            builder.Property(fq => fq.QuestionOrder).IsRequired();

            //builder.HasOne(fq => fq.Form).WithMany(f => f.FormQuestions).HasForeignKey(fq => fq.FormID).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(fq => fq.Question).WithMany(q => q.FormQuestions).HasForeignKey(fq => fq.QuestionID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(fp => fp.FormQuestionProcesses).WithOne(fqp => fqp.FormQuestion).HasForeignKey(fqp => fqp.FormQuestionID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
