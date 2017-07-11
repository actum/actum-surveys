using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.DA
{
    public class SurveyReportViewDto
    {
        public int SubmittedCount { get; set; }
        public SurveyReportQuestionDto[] AnswersDetails { get; set; }
    }
}
