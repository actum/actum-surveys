using System.Linq;

namespace Surveys.DA
{
    public class SurveyReportQuestionDto
    {
        public int QuestionId { get; set; }
        public int TotalPositive => Details.Count(d => d.IsPositive);
        public int TotalNegative => Details.Count(d => d.IsNegative);
        public int TotalComments => Details.Count(d => d.HasComment);
        public SurveyReportAnswerDetailDto[] Details { get; set; }
        public string Text { get; internal set; }
        public string GroupText { get; internal set; }
        public bool ShouldShowThumbs => BO.SurveyBO.FuncShouldShowThumbs(Type);
        public SurveyQuestionType Type { get; internal set; }
    }
}