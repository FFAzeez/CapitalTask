using CapitalPlacementTask.Domain.Enums;
using CapitalPlacementTaskAPI.Domain.BindingModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Business.Commands
{
    public class UpdateProgramDetailCommand:IRequest<ServiceResponse>
    {
        [Required]
        public long Id { get;set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string KeySkills { get; set; }
        public string Benefits { get; set; }
        public string ApplicationCriteria { get; set; }
        public AdditionalProgramInformationCommand? AdditionalProgramInformation { get; set; }
    }
    public class UpdateAdditionalProgramInformation
    {
        public ProgramType ProgramType { get; set; }
        public DateTime ProgramStart { get; set; }
        public DateTime ApplicationOpen { get; set; }
        public DateTime ApplicationClose { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public bool ModeWork { get; set; }
        public QualificationType? MinQualificcation { get; set; }
        public int? MaxApplicationNumber { get; set; }

    }
}
