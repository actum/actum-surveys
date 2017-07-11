using Surveys.BO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Surveys.DA;
using Microsoft.EntityFrameworkCore;

namespace Surveys
{
    public class SurveyService
    {
        DA.SurveysDataContext _dc;

        public SurveyService(DA.SurveysDataContext dc)
        {
            _dc = dc;
        }

        public SurveyBO GetSurveyByUrl(string url)
        {
            var survey = _dc.Surveys
                .Select(s =>
                new SurveyFetchDto()
                {
                    Survey = s,
                    Questions = s.Questions.Select(q =>
                    new SurveyQuestionFetchDto()
                    {
                        Question = q,
                        Group = q.Group
                    }).ToList()
                }).SingleOrDefault(s => s.Survey.Url == url);

            if (survey == null)
                return null;

            foreach (var q in survey.Questions)
            {
                var entry = _dc.Entry(q.Question); 
                entry.State = EntityState.Unchanged;
            }

            return new SurveyBO(survey);
        }

        public void SaveAnswers(SurveyBO survey, ClientIdentityBO client)
        {
            // new answers?
            if (survey.Answers.Id == 0)
            {
                // apply client info
                survey.Answers.Data.ClientId = client.Id;

                _dc.SurveyAnswers.Add(survey.Answers.Data);
            }

            _dc.SaveChanges();
        }

        public void FetchAnswersForClient(SurveyBO survey, ClientIdentityBO resolveClientId)
        {
            var answers = _dc.SurveyAnswers
                .Include(a => a.Answers)
                .FirstOrDefault(sa => sa.Survey.Id == survey.Id && sa.ClientId == resolveClientId.Id && !resolveClientId.IsAnonymous);

            if (answers == null)
            {
                answers = new SurveyAnswer()
                {
                    Created = DateTimeLocal.Now,
                    Answers = new List<SurveyQuestionAnswer>(),
                    Survey = survey.Data
                };
            }

            // question answers
            survey.Answers = new SurveyAnswersBO(answers);
            survey.Answers.Answers = answers.Answers.ToDictionary(
                    a => a.Question.Id,
                    a => new SurveyQuestionAnswerBO(a)
                );

            // add not yet existing answers
            foreach (var q in survey.Questions.Where(q => !survey.Answers.Answers.ContainsKey(q.Id)))
            {
                var answer = new SurveyQuestionAnswer();
                answer.Question = q.Data;
                answer.SurveyAnswer = survey.Answers.Data;

                // add to answer entity
                survey.Answers.Data.Answers.Add(answer);
                survey.Answers.Answers.Add(q.Id, new SurveyQuestionAnswerBO(answer));
            }
        }

        public SurveyReportBO GenerateReport(SurveyBO survey)
        {
            var result = new SurveyReportBO();
            result.Survey = survey;

            var answersQuery = _dc.SurveyAnswers
                .Where(sa => sa.Survey.Id == survey.Id);

            var questionsQuery = _dc.SurveyQuestions
                .Where(sa => sa.Survey.Id == survey.Id);

            result.ReportData = new SurveyReportViewDto();
            result.ReportData.SubmittedCount = answersQuery.Count();

            // this is just simplified sample - it also does request for each row
            //var test = _dc.SurveyQuestions
            //    .Include(qa => qa.Answers)
            //    .Select(qa => new
            //    {
            //        Text = qa.Text,
            //        Answers = qa.Answers.Select(a => new {
            //            Value = a.Value
            //        }).ToArray()
            //    }).ToArray();

            result.ReportData.AnswersDetails = questionsQuery
                .Include(qa => qa.Answers)
                .Select(qa => new SurveyReportQuestionDto()
                {
                    QuestionId = qa.Id,
                    Text = qa.Text,
                    GroupText = qa.Group.Name,
                    Type = qa.Type,
                    Details = qa.Answers.Select(a => new SurveyReportAnswerDetailDto()
                    {
                        TextValue = a.TextValue,
                        Value = a.Value
                    }).ToArray()
                }).ToArray();

            return result;
        }
    }

}
