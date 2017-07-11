using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.DA
{
    public class SurveysDataContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<SurveyQuestionAnswer> SurveyQuestionAnswers { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }

        public SurveysDataContext(DbContextOptions<SurveysDataContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Survey>()
                .HasMany(s => s.Questions).WithOne(sq => sq.Survey)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SurveyQuestion>()
                .HasMany(sq => sq.Answers).WithOne(sqa => sqa.Question)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
