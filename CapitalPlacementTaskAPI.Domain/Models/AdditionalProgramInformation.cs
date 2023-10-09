using CapitalPlacementTask.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("AdditionalProgramInformations")]
    public class AdditionalProgramInformation : BaseModel
    {
        [Required]
        public ProgramType ProgramType { get; set; }
        public DateTime ProgramStart { get; set; }
        [Required]
        public DateTime ApplicationOpen { get; set; }
        [Required]
        public DateTime ApplicationClose { get; set; }
        public string Duration { get; set; }
        [Required]
        public string Location { get; set; }
        public bool ModeWork { get; set; }
        public QualificationType? MinQualificcation { get; set; }
        public int? MaxApplicationNumber { get; set; }
        [ForeignKey("ProgramDetails")]
        public long ProgramDetailId { get; set; }
        public ProgramDetail ProgramDetail { get; set; }
    }
}
