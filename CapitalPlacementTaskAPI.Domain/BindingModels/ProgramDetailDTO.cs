using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.BindingModels
{
    public class ProgramDetailDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string KeySkills { get; set; }
        public string Benefits { get; set; }
        public string ApplicationCriteria { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public AdditionalProgramInformationDTO AdditionalProgramInformation { get; set; }
    }
    public class AdditionalProgramInformationDTO
    {
        public long Id { get; set; }
        public string ProgramType { get; set; }
        public DateTime ProgramStart { get; set; }
        public DateTime ApplicationOpen { get; set; }
        public DateTime ApplicationClose { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public bool ModeWork { get; set; }
        public string MinQualificcation { get; set; }
        public int MaxApplicationNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
