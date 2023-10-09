using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("ProgramDetails")]
    public class ProgramDetail:BaseModel
    {
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }
        public string KeySkills { get; set; }
        public string Benefits { get; set; }
        public string ApplicationCriteria { get; set; }
        public virtual AdditionalProgramInformation AdditionalProgramInformation { get; set; }
    }
}
