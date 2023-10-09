using CapitalPlacementTaskAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("PersonalInformations")]
    public class PersonalInformation:BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<SecondaryPersonalInformation> SecondaryPersonalInformation { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }
        [ForeignKey("ApplicationForms")]
        public long ApplicationFormId { get; set; }
    }
}
