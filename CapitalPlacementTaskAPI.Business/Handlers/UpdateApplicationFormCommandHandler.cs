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
    public class UpdateApplicationFormCommandHandler : IRequestHandler<UpdateApplicationFormCommand, ServiceResponse>
    {
        readonly IUnitOfWork _uow;
        public UpdateApplicationFormCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ServiceResponse> Handle(UpdateApplicationFormCommand request, CancellationToken cancellationToken)
        {
            var programDetail = _uow.GetRepository<ApplicationForm>().Get(_ => _.Id == request.Id, includeProperties: "ProgramDetail,Document,PersonalInformation,Profile,AdditionalQuestion").FirstOrDefault();
            if (programDetail == null)
            {
                return new ServiceResponse
                {
                    StatusCode = ResponseCode.NOTFOUND,
                    StatusMessage = "Program detail not found"
                };
            }
            var 
        }
    }
}
