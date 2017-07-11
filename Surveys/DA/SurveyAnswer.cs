using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys.DA
{
    public class SurveyAnswer
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual Survey Survey { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        public virtual ICollection<SurveyQuestionAnswer> Answers { get; set; }
    }
}