using CapitalPlacementTaskAPI.Domain.BindingModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Business.Queries
{
    public class GetAllProgramDetailQuery:IRequest<GenericListSearchResult<IEnumerable<ProgramDetailDTO>>>
    {
        public string? Search { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public int PageSize { get; set; }
        [Required]
        public int PageNumber { get; set; }
    }
}
