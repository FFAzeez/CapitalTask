using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("EducationModels")]
    public class EducationModel:BaseModel
    {
        public string School { get; set; }
        public string Degree { get; set; }
        public string Course { get; set; }
        public string Location { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyStudying { get; set; }
        [ForeignKey("Educations")]
        public long EducationId { get; set; }
        public Education Education { get; set; }
    }
}
