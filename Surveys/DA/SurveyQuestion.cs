using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys.DA
{
    public class SurveyQuestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual Survey Survey { get; set; }

        [Required]
        public SurveyQuestionType Type { get; set; }

        [MaxLength(500), Required]
        public string Text { get; set; }

        [Column("SurveyQuestionGroup_Id")]
        public int SurveyQuestionGroupId { get; set; }

        [Required]
        public virtual SurveyQuestionGroup Group { get; set; }

        public virtual ICollection<SurveyQuestionAnswer> Answers { get; set; }

        public int Order { get; set; }
    }
}