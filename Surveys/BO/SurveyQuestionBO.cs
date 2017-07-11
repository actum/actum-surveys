using Surveys.DA;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Surveys.BO
{
    public class SurveyQuestionBO
    {
        public SurveyQuestionBO(SurveyQuestionFetchDto question, SurveyQuestionGroupBO group)
        {
            Data = question.Question;
            Group = group;
        }

        public SurveyQuestion Data { get; private set; }

        public int Id { get { return Data.Id; } }

        public SurveyQuestionType Type { get { return Data.Type; } }

        public string Text { get { return Data.Text; } }

        public int Order { get { return Data.Order; } }

        public SurveyQuestionGroupBO Group { get; set; }

        public List<SurveyQuestionAnswerBO> Answers { get; set; }

        public IEnumerable<SurveyQuestionAnswerBO> AnswersWithTextValue
        {
            get { return Answers.Where(a => a.HasTextValue); }
        }

        public int GetPercentsForValue(int value)
        {
            var answersCount = (double)Answers.Count();
            if (answersCount == 0)
                return 0;

            var answersWithValueCount = (double)Answers.Count(a => a.Value == value);

            return (int)(100 * answersWithValueCount / answersCount);
        }

        public int GetCountForValue(int value)
        {
            return Answers.Count(a => a.Value == value);
        }

        public int TotalVotes
        {
            get {
                return Answers.Count(a => a.Value != 0);
            }
        }
    }
}