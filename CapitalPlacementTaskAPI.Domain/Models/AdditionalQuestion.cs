using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("AdditionalQuestions")]
    public class AdditionalQuestion:BaseModel
    {
        [Required]
        public string AboutYou { get; set; }
        public string Year { get; set; }
        public bool IsRejected { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }
        [ForeignKey("ApplicationForms")]
        public long ApplicationFormId { get; set; }
    }
}
