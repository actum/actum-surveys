namespace Surveys.DA
{
    public class SurveyReportAnswerDetailDto
    {
        public bool HasComment => !string.IsNullOrWhiteSpace(TextValue);
        public bool IsNegative => Value == SurveyAnswerValues.Negative;
        public bool IsPositive => Value == SurveyAnswerValues.Positive;
        public string TextValue { get; set; }
        public int Value { get; set; }
    }
}