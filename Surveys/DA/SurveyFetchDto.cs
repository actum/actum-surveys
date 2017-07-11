using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.DA
{
    public class SurveyFetchDto
    {
        public Survey Survey { get; set; }

        public ICollection<SurveyQuestionFetchDto> Questions { get; set; }
        public SurveyQuestion Test { get; set; }
    }
}
