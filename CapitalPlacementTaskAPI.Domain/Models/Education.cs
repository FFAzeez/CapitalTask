using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("Educations")]
    public class Education:BaseModel
    {
        public bool IsMandatory { get; set; }
        public bool IsShow { get; set; }
        public IEnumerable<EducationModel> Educations { get; set; }
        [ForeignKey("Profiles")]
        public long ProfileId { get; set; }
    }
}
