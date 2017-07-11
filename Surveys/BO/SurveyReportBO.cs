using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.BO
{
    public class SurveyReportBO
    {
        public SurveyBO Survey { get; set; }
        public DA.SurveyReportViewDto ReportData { get; set; }
    }
}
