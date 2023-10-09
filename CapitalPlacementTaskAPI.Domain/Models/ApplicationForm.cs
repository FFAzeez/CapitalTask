using CapitalPlacementTaskAPI.Domain.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("ApplicationForms")]
    public class ApplicationForm:BaseModel
    {
        [ForeignKey("ProgramDetails")]
        public long ProgramDetailId { get; set; }
        public virtual ProgramDetail ProgramDetail { get; set; }
        public Document Document { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public Profile Profile { get; set; }
        public AdditionalQuestion AdditionalQuestion { get; set; }
    }
}
