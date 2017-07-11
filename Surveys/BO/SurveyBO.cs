using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveys.DA;

namespace Surveys.BO
{
    public class SurveyBO
    {
        public SurveyBO(SurveyFetchDto survey)
        {
            Data = survey.Survey;

            // build questions
            var questions = survey.Questions ?? Enumerable.Empty<SurveyQuestionFetchDto>();
            var questionsBOs = new List<SurveyQuestionBO>(questions.Count());

            var groups = new Dictionary<int, SurveyQuestionGroupBO>();
            Func<SurveyQuestionGroup, SurveyQuestionGroupBO> groupResolve = (g) =>
            {
                SurveyQuestionGroupBO result;
                if(!groups.TryGetValue(g.Id, out result))
                {
                    groups.Add(g.Id, result = new SurveyQuestionGroupBO(g));
                }
                return result;
            };

            foreach (var question in questions.OrderBy(q => q.Question.Order))
            {
                SurveyQuestionGroupBO group = groupResolve(question.Group);
                var qBO = new SurveyQuestionBO(question, group);
                questionsBOs.Add(qBO);
            }
            Questions = questionsBOs;
        }

        /// <summary>
        /// Returns true if given question type supports voting thumbs up/down.
        /// </summary>
        /// <param name="type">Type of question.</param>
        /// <returns>True if thumbs up/down is supported.</returns>
        public static bool FuncShouldShowThumbs(SurveyQuestionType type)
        {
            switch (type)
            {
                case SurveyQuestionType.FreeText:
                    return false;
                default:
                    return true;
            }
        }

        public ICollection<SurveyQuestionBO> Questions { get; set; }

        public SurveyAnswersBO Answers { get; set; }

        public Survey Data { get; private set; }

        public int Id { get { return Data.Id; } }

        public string Name { get { return Data.Name; } }

        public bool IsClosed { get { return Data.CloseDate <= DateTimeLocal.Now; } }

        public bool CanBeAnsweredAnonymously
        {
            get
            {
                return true;
            }
        }

        public string Url => Data.Url;

        public DateTime CloseDate => Data.CloseDate;

        public DateTime CreatedDate => Data.CreatedDate;
    }
}
