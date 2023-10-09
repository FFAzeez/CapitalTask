using AutoMapper;
using CapitalPlacementTaskAPI.Business.Commands;
using CapitalPlacementTaskAPI.Domain.BindingModels;
using CapitalPlacementTaskAPI.Domain.Const;
using CapitalPlacementTaskAPI.Domain.Models;
using CapitalPlacementTaskAPI.Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Business.Handlers
{
    public class UpdateProgramDetailCommandHandler:IRequestHandler<UpdateProgramDetailCommand,ServiceResponse>
    {
        readonly IUnitOfWork _uow;
        public UpdateProgramDetailCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ServiceResponse> Handle(UpdateProgramDetailCommand request, CancellationToken cancellationToken)
        {
            var programDetail = _uow.GetRepository<ProgramDetail>().Get(_ => _.Id == request.Id, includeProperties: "AdditionalProgramInformation").FirstOrDefault();
            if(programDetail == null)
            {
                return new ServiceResponse
                {
                    StatusCode = ResponseCode.NOTFOUND,
                    StatusMessage = "Program detail not found"
                };
            }

            using (var transaction = _uow.BeginTransaction())
            {
                programDetail.Summary = request.Summary;
                programDetail.ApplicationCriteria = request.ApplicationCriteria;
                programDetail.Title = request.Title;
                programDetail.Benefits = request.Benefits;
                programDetail.KeySkills = request.KeySkills;
                programDetail.Description = request.Description;
                if (request.AdditionalProgramInformation != null && programDetail.AdditionalProgramInformation != null)
                {
                    programDetail.AdditionalProgramInformation.ApplicationClose = request.AdditionalProgramInformation.ApplicationClose;
                    programDetail.AdditionalProgramInformation.ApplicationOpen = request.AdditionalProgramInformation.ApplicationOpen;
                    programDetail.AdditionalProgramInformation.ProgramStart = request.AdditionalProgramInformation.ProgramStart;
                    programDetail.AdditionalProgramInformation.ProgramType = request.AdditionalProgramInformation.ProgramType;
                    programDetail.AdditionalProgramInformation.Duration = request.AdditionalProgramInformation.Duration;
                    programDetail.AdditionalProgramInformation.Location = request.AdditionalProgramInformation.Location;
                    programDetail.AdditionalProgramInformation.MinQualificcation = request.AdditionalProgramInformation.MinQualificcation;
                    programDetail.AdditionalProgramInformation.MaxApplicationNumber = request.AdditionalProgramInformation.MaxApplicationNumber;
                    programDetail.AdditionalProgramInformation.ModeWork = request.AdditionalProgramInformation.ModeWork;
                }
                else if(request.AdditionalProgramInformation != null && programDetail.AdditionalProgramInformation == null)
                {
                    programDetail.AdditionalProgramInformation = new AdditionalProgramInformation
                    {
                        ModeWork = request.AdditionalProgramInformation.ModeWork,
                        Duration = request.AdditionalProgramInformation.Duration,
                        ProgramType = request.AdditionalProgramInformation.ProgramType,
                        ProgramStart = request.AdditionalProgramInformation.ProgramStart,
                        Location = request.AdditionalProgramInformation.Location,
                        MinQualificcation = request.AdditionalProgramInformation.MinQualificcation,
                        MaxApplicationNumber = request.AdditionalProgramInformation.MaxApplicationNumber,
                        ApplicationClose = request.AdditionalProgramInformation.ApplicationClose,
                        ApplicationOpen = request.AdditionalProgramInformation.ApplicationOpen,
                    };
                }
                _uow.GetRepository<ProgramDetail>().Update(programDetail);
                _uow.Save();
                transaction.Commit();
            }
            return new ServiceResponse
            {
                StatusCode = ResponseCode.SUCCESSFUL,
                StatusMessage = "Program details successfully updated."
            };
        }
    }
}
