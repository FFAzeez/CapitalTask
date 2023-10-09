using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("WorkExperienceModels")]
    public class WorkExperienceModel:BaseModel
    {
        public string Company { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyWorking { get; set; }
        [ForeignKey("WorkExperiences")]
        public bool WorkExperienceId { get; set; }
        public WorkExperience WorkExperience { get; set; }
    }
}
