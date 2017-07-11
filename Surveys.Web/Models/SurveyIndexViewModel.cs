using Surveys.BO;
using Surveys.DA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Web.Models
{
    public class SurveyIndexViewModel
    {
        public SurveyIndexViewModel(SurveyBO survey)
        {
            Survey = survey;
            Answers = new Dictionary<string, SurveyQuestionAnswerViewModel>();
            FillSessions();
        }

        private void FillSessions()
        {
            // create sections
            Sections = Survey.Questions.GroupBy(q => q.Group)
                .Select(g => new SurveySectionViewModel()
                {
                    SectionHeader = g.Key.Name,
                    Questions = g.Select(gi => new SurveyQuestionViewModel(gi)).ToList()
                }).ToList();

            // fetch existing answers
            foreach (var a in Survey.Answers.Answers)
            {
                Answers.Add(a.Key.ToString(), new SurveyQuestionAnswerViewModel()
                {
                    QuestionId = a.Key,
                    TextValue = a.Value.TextValue,
                    IntValue = a.Value.Value
                });
            }
        }


        public SurveyBO Survey { get; private set; }

        public IList<SurveySectionViewModel> Sections { get; private set; }

        public IDictionary<string, SurveyQuestionAnswerViewModel> Answers { get; set; }

        public void UpdateBO()
        {
            foreach (var q in Survey.Answers.Answers.Join(Answers, qi => qi.Key, qi => int.Parse(qi.Key),
                (q, m) => new { TargetBO = q, Model = m }))
            {
                var val = q.Model.Value;
                var target = q.TargetBO.Value;
                target.Value = val.IntValue;
                target.TextValue = val.TextValue;
            }
        }
    }

    public class SurveySectionViewModel
    {
        public string SectionHeader { get; set; }
        public IList<SurveyQuestionViewModel> Questions { get; set; }
    }

    public class SurveyQuestionAnswerViewModel
    {
        public int QuestionId { get; set; }

        [StringLength(500)]
        public string TextValue { get; set; }

        public int IntValue { get; set; }
    }

    public class SurveyQuestionViewModel
    {
        public SurveyQuestionViewModel(SurveyQuestionBO q)
        {
            Id = q.Id;
            Text = q.Text;
            Type = q.Type;
        }

        public string Text { get; set; }

        public int Id { get; set; }

        public SurveyQuestionType Type { get; set; }
    }
}
