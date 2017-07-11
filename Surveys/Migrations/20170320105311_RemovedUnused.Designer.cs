using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Surveys.DA;

namespace Surveys.Migrations
{
    [DbContext(typeof(SurveysDataContext))]
    [Migration("20170320105311_RemovedUnused")]
    partial class RemovedUnused
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Surveys.DA.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CloseDate");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Surveys.DA.SurveyAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ClientId");

                    b.Property<DateTime>("Created");

                    b.Property<int>("SurveyId");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyAnswers");
                });

            modelBuilder.Entity("Surveys.DA.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Order");

                    b.Property<int>("SurveyId");

                    b.Property<int>("SurveyQuestionGroupId")
                        .HasColumnName("SurveyQuestionGroup_Id");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.HasIndex("SurveyQuestionGroupId");

                    b.ToTable("SurveyQuestions");
                });

            modelBuilder.Entity("Surveys.DA.SurveyQuestionAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("QuestionId")
                        .HasColumnName("Question_Id");

                    b.Property<int>("SurveyAnswerId");

                    b.Property<string>("TextValue")
                        .HasMaxLength(5000);

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SurveyAnswerId");

                    b.ToTable("SurveyQuestionAnswers");
                });

            modelBuilder.Entity("Surveys.DA.SurveyQuestionGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SurveyQuestionGroup");
                });

            modelBuilder.Entity("Surveys.DA.SurveyAnswer", b =>
                {
                    b.HasOne("Surveys.DA.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Surveys.DA.SurveyQuestion", b =>
                {
                    b.HasOne("Surveys.DA.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId");

                    b.HasOne("Surveys.DA.SurveyQuestionGroup", "Group")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyQuestionGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Surveys.DA.SurveyQuestionAnswer", b =>
                {
                    b.HasOne("Surveys.DA.SurveyQuestion", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId");

                    b.HasOne("Surveys.DA.SurveyAnswer", "SurveyAnswer")
                        .WithMany("Answers")
                        .HasForeignKey("SurveyAnswerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
