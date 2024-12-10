using FormMaker.Data.Configuration;
using FormMaker.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FormMaker.Data.Context
{
    public class FormMakerDbContext : DbContext
    {
        public FormMakerDbContext(DbContextOptions<FormMakerDbContext> options) : base(options) { }

        // DbSets for your entities
        public DbSet<Question> Questions { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormQuestion> FormQuestions { get; set; }
        public DbSet<FormProcess> FormProcesses { get; set; }
        public DbSet<FormQuestionProcess> FormQuestionProcesses { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new ProcessConfiguration());
            modelBuilder.ApplyConfiguration(new FormConfiguration());
            modelBuilder.ApplyConfiguration(new FormQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new FormProcessConfiguration());
            modelBuilder.ApplyConfiguration(new FormQuestionProcessConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerOptionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        }
    }
}
