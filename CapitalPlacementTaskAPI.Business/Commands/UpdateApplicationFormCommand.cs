using CapitalPlacementTaskAPI.Domain.BindingModels;
using CapitalPlacementTaskAPI.Domain.Enums;
using CapitalPlacementTaskAPI.Domain.Models;
using CapitalPlacementTaskAPI.Domain.Utility;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Business.Commands
{
    public class UpdateApplicationFormCommand:IRequest<ServiceResponse>
    {
        [Required]
        public long Id { get; set; }
        [Required, MaxFileSize(1024)]
        public IFormFile UploadImage { get; set; }
        public PersonalInformationCommand PersonalInformation { get; set; }
        public ProfileCommand Profile { get; set; }
        public AdditionalQuestionCommand AdditionalQuestion { get; set; }
    }
    public class PersonalInformationCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<SecondaryPersonalInformationCommand> SecondaryPersonalInformation { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }
    }

    public class SecondaryPersonalInformationCommand
    {
        [Required]
        public SecondaryInformationType Type { get; set; }
        [Required]
        public string Value { get; set; }
        public bool IsInternal { get; set; }
        public bool IsShow { get; set; }
    }

    public class ProfileCommand
    {
        public EducationCommand Education { get; set; }
        public WorkExperienceCommand WorkExperience { get; set; }
        public IFormFile ResumeFile { get; set; }
        public IEnumerable<QuestionCommand> Questions { get; set; }
    }

    public class EducationCommand
    {
        public bool IsMandatory { get; set; }
        public bool IsShow { get; set; }
        public IEnumerable<EducationModelCommand> Educations { get; set; }
    }
    public class EducationModelCommand
    {
        public string School { get; set; }
        public string Degree { get; set; }
        public string Course { get; set; }
        public string Location { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyStudying { get; set; }
    }

    public class AdditionalQuestionCommand
    {
        [Required]
        public string AboutYou { get; set; }
        public string Year { get; set; }
        public bool IsRejected { get; set; }
        public IEnumerable<QuestionCommand> Questions { get; set; }
    }

    public class WorkExperienceCommand
    {
        public bool IsMandatory { get; set; }
        public bool IsShow { get; set; }
        public IEnumerable<WorkExperienceModelCommand> WorkExperiences { get; set; }
    }

    public class WorkExperienceModelCommand
    {
        public string Company { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyWorking { get; set; }
    }

    public class QuestionCommand
    {
        [Required]
        public QuestionType QuestionType { get; set; }
        [Required]
        public string Question { get; set; }
        public string? Choice { get; set; }
        public bool? IsOption { get; set; }
        public bool? IsDisqualify { get; set; }
        public int? MaxChoice { get; set; }
    }
}
