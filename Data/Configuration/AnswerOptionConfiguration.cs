using FormMaker.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FormMaker.Data.Configuration
{
    public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
    {
        public void Configure(EntityTypeBuilder<AnswerOption> builder)
        {
            builder.HasKey(q => q.OptionID);
            builder.Property(q => q.OptionID).ValueGeneratedOnAdd().IsRequired();

            builder.Property(ao => ao.OptionText).IsRequired().HasMaxLength(200);

            //builder.HasMany(ao => ao.Answers).WithOne(a => a.AnswerOption).HasForeignKey(a => a.AnswerOptionID);
            builder.HasOne(ao => ao.Question).WithMany(q => q.AnswerOptions).HasForeignKey(ao => ao.QuestionID);
        }
    }
}
