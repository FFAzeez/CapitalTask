using AutoMapper;
using CapitalPlacementTaskAPI.Business.Queries;
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
    public class GetProgramDetailByIdQueryHandler:IRequestHandler<GetProgramDetailByIdQuery,ServiceResponse<ProgramDetailDTO>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetProgramDetailByIdQueryHandler(IUnitOfWork uow, IMapper map)
        {
            _uow = uow;
            _mapper = map;
        }

        public async Task<ServiceResponse<ProgramDetailDTO>> Handle(GetProgramDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var programDetail = _uow.GetRepository<ProgramDetail>().Get(_ => _.Id == request.Id, includeProperties: "AdditionalProgramInformation").FirstOrDefault();
            if (programDetail == null)
            {
                return ServiceResponse<ProgramDetailDTO>.Failed(null,ResponseCode.NOTFOUND);
            }
            var response =_mapper.Map<ProgramDetailDTO>(programDetail);
            return ServiceResponse<ProgramDetailDTO>.Success(response);
        }
    }
}
