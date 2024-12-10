using FormMaker.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Data.Configuration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(q => q.AnswerID);
            builder.Property(q => q.AnswerID).ValueGeneratedOnAdd().IsRequired();

            builder.Property(a => a.AnswerText).HasMaxLength(1000);
            builder.Property(a => a.FilePath).HasMaxLength(500);

            builder.HasOne(a => a.AnswerOption).WithMany(ao => ao.Answers).HasForeignKey(a => a.AnswerOptionID).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(a => a.FormQuestionProcess).WithMany().HasForeignKey(a => a.FormQuestionProcessID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
