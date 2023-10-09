using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("")]
    public class WorkExperience:BaseModel
    {
        public bool IsMandatory { get; set; }
        public bool IsShow { get; set; }
        public IEnumerable<WorkExperienceModel> WorkExperiences { get; set; }
        [ForeignKey("ApplicationForms")]
        public long ApplicationFormId { get; set; }
    }
}
