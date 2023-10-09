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
    public class CreateProgramDetailCommandHandler : IRequestHandler<CreateProgramDetailCommand, ServiceResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CreateProgramDetailCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> Handle(CreateProgramDetailCommand request, CancellationToken cancellationToken)
        {
            using(var transaction = _uow.BeginTransaction())
            {
                var resultMap = _mapper.Map<ProgramDetail>(request);
                _uow.GetRepository<ProgramDetail>().Insert(resultMap);
                _uow.Save();
                transaction.Commit();
            }
            return new ServiceResponse
            {
                StatusCode = ResponseCode.SUCCESSFUL,
                StatusMessage = "Program details successfully created."
            };
        }
    }
}
