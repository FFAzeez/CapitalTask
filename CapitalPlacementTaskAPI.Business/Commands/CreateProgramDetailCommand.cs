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
    public class CreateProgramDetailCommand:IRequest<ServiceResponse>
    {
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }
        public string? KeySkills { get; set; }
        public string? Benefits { get; set; }
        public string? ApplicationCriteria { get; set; }
        public AdditionalProgramInformationCommand? AdditionalProgramInformation { get; set; }
    }

    public class AdditionalProgramInformationCommand
    {
        [Required]
        public ProgramType ProgramType { get; set; }
        public DateTime ProgramStart { get; set; }
        [Required]
        public DateTime ApplicationOpen { get; set; }
        [Required]
        public DateTime ApplicationClose { get; set; }
        public string Duration { get; set; }
        [Required]
        public string Location { get; set; }
        public bool ModeWork { get; set; }
        public QualificationType? MinQualificcation { get; set; }
        public int? MaxApplicationNumber { get; set; }

    }
}
