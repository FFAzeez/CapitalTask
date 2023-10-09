using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    public class Document:BaseModel
    {
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public string Url { get; set; }
        [ForeignKey("ApplicationForms")]
        public long ApplicationFormId { get; set; }
        public ApplicationForm ApplicationForm { get; set; }
    }
}
