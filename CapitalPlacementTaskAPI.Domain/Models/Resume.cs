using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Models
{
    public class Resume:BaseModel
    {
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public string Url { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsShow { get; set; }
    }
}
