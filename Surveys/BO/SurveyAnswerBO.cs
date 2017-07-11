using Surveys.DA;
using System.Collections.Generic;

namespace Surveys.BO
{
    public class SurveyAnswersBO
    {
        public SurveyAnswersBO(SurveyAnswer answers)
        {
            Answers = new Dictionary<int, SurveyQuestionAnswerBO>();
            this.Data = answers;
        }

        public Dictionary<int, SurveyQuestionAnswerBO> Answers { get; set; }

        public SurveyAnswer Data { get; private set; }

        public int Id { get { return Data.Id; } }
    }
}