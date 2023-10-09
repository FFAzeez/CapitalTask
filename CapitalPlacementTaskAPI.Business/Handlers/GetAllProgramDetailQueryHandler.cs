using AutoMapper;
using CapitalPlacementTaskAPI.Business.Queries;
using CapitalPlacementTaskAPI.Domain.BindingModels;
using CapitalPlacementTaskAPI.Domain.Const;
using CapitalPlacementTaskAPI.Domain.Models;
using CapitalPlacementTaskAPI.Domain.Utility;
using CapitalPlacementTaskAPI.Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Business.Handlers
{
    public class GetAllProgramDetailQueryHandler:IRequestHandler<GetAllProgramDetailQuery, GenericListSearchResult<IEnumerable<ProgramDetailDTO>>>
    {
        readonly IUnitOfWork _uow;
        readonly IMapper _mapper;
        public GetAllProgramDetailQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericListSearchResult<IEnumerable<ProgramDetailDTO>>> Handle(GetAllProgramDetailQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ProgramDetail, bool>> predicate = x => true;

            if (!string.IsNullOrEmpty(request.Search))
            {
                predicate.And(_ => _.Title.Contains(request.Search) || _.ApplicationCriteria.Contains(request.Search) || _.KeySkills.Contains(request.Search)
                 || _.Benefits.Contains(request.Search) || _.Description.Contains(request.Search) || _.Summary.Contains(request.Search));
            }

            if (!string.IsNullOrEmpty(request.StartDate.ToString()))
            {
                predicate = predicate.And(_ => _.CreatedDate >= request.StartDate);
            }


            if (!string.IsNullOrEmpty(request.EndDate.ToString()))
            {
                predicate = predicate.And(_ => _.CreatedDate <= request.EndDate);
            }

            var result = _uow.GetRepository<ProgramDetail>().Get(predicate, _ => _.OrderByDescending(_ => _.Id), includeProperties: "AdditionalProgramInformation");
            if (result.Any())
            {
                var map = _mapper.Map<IEnumerable<ProgramDetailDTO>>(result);
                var response = Pagination<ProgramDetailDTO>.ToPagedList(map, request.PageNumber, request.PageSize);
                return new GenericListSearchResult<IEnumerable<ProgramDetailDTO>>()
                {
                    StatusCode = ResponseCode.SUCCESSFUL,
                    StatusMessage = "Program detail program successfully fetched",
                    Result = response,
                    CurrentPage = response.CurrentPage,
                    PageSize = response.PageSize,
                    TotalPages = response.TotalPages,
                    TotalRows = response.TotalCount
                };
            }
            return new GenericListSearchResult<IEnumerable<ProgramDetailDTO>>
            {
                StatusCode = ResponseCode.NOTFOUND,
                StatusMessage = "No program detail found",
            };
        }
    }
}
