using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.DA
{
    public class SurveyQuestionFetchDto
    {
        public SurveyQuestion Question { get; set; }
        public SurveyQuestionGroup Group { get; set; }
    }
}
