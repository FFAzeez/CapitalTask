using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("Profiles")]
    public class Profile:BaseModel
    {
        public Education Education { get; set; }
        public WorkExperience WorkExperience { get; set; }
        public Resume Resume { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }
        [ForeignKey("ApplicationForms")]
        public long ApplicationFormId { get; set; }
    }
}
