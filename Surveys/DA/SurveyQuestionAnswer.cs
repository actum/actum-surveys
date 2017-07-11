using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys.DA
{
    public class SurveyQuestionAnswer
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Question_Id")]
        public int QuestionId { get; set; }

        [Required]
        public virtual SurveyQuestion Question { get; set; }
        
        [Required]
        public virtual SurveyAnswer SurveyAnswer { get; set; }

        public int Value { get; set; }

        [MaxLength(5000)]
        public string TextValue { get; set; }
    }
}