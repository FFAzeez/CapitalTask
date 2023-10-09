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
    public class GetProgramDetailByIdQuery:IRequest<ServiceResponse<ProgramDetailDTO>>
    {
        [Required]
        public long Id { get; set; }
    }
}
