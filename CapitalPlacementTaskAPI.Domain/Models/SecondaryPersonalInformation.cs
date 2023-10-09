using CapitalPlacementTaskAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    [Table("SecondaryPersonalInformations")]
    public class SecondaryPersonalInformation:BaseModel
    {
        public SecondaryInformationType Type { get; set; }
        public string Value { get; set; }
        public bool IsInternal { get; set; }
        public bool IsShow { get; set; }
        [ForeignKey("PersonalInformations")]
        public long PersonalInformationId { get; set; }
    }
}
