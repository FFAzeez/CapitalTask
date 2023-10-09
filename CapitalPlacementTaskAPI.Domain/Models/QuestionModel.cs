using CapitalPlacementTaskAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("QuestionModels")]
    public class QuestionModel:BaseModel
    {
        [Required]
        public QuestionType QuestionType { get; set; }
        [Required]
        public string Question { get; set; }
        public string? Choice { get; set; }
        public bool? IsOption { get; set; }
        public bool? IsDisqualify { get; set; }
        public int? MaxChoice { get; set; }
        [ForeignKey("PersonalInformations")]
        public long PersonalInformationId { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }
        [ForeignKey("Profiles")]
        public long ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        [ForeignKey("AdditionalQuestions")]
        public long AdditionalQuestionId { get; set; }
        public virtual AdditionalQuestion AdditionalQuestion { get; set; }
    }
}
