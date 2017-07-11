using Surveys.DA;

namespace Surveys.BO
{
    public class SurveyQuestionAnswerBO
    {
        public SurveyQuestionAnswerBO(SurveyQuestionAnswer answer)
        {
            this.Data = answer;
        }

        public SurveyQuestionAnswer Data { get; private set; }

        public bool HasTextValue { get { return !string.IsNullOrWhiteSpace(TextValue); } }

        public int Id { get { return Data.Id; } }

        public string TextValue { get { return Data.TextValue; } set { Data.TextValue = value; } }

        public int Value { get { return Data.Value; } set { Data.Value = value; } }
    }
}