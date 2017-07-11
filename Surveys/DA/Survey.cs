using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.DA
{
    public class Survey
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public DateTime CloseDate { get; set; }

        [Required]
        public string Url { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<SurveyQuestion> Questions { get; set; }
    }
}
