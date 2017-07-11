using Surveys.DA;

namespace Surveys.BO
{
    public class SurveyQuestionGroupBO
    {
        public SurveyQuestionGroupBO(SurveyQuestionGroup group)
        {
            Id = group.Id;
            Name = group.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}