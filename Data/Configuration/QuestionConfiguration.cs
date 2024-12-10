using FormMaker.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Data.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.QuestionID);
            builder.Property(q => q.QuestionID).ValueGeneratedOnAdd().IsRequired();

            builder.HasIndex(q => q.QuestionTitle);

            builder.Property(q => q.QuestionTitle).IsRequired().HasMaxLength(400);
            builder.Property(q => q.QuestionType).HasMaxLength(100);

            //builder.HasMany(q => q.AnswerOptions).WithOne(ao => ao.Question).HasForeignKey(ao => ao.QuestionID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(q => q.FormQuestions).WithOne(fq => fq.Question).HasForeignKey(fq => fq.QuestionID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
