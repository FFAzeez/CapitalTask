using CapitalPlacementTaskAPI.Business.Commands;
using CapitalPlacementTaskAPI.Domain.BindingModels;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramDetailController : BaseController
    {
        private readonly ILogger<ProgramDetailController> _logger;
        private readonly string _env;
        private readonly IMediator _mediator;
        public ProgramDetailController(ILogger<ProgramDetailController> logger, IWebHostEnvironment environment, IMediator mediator)
        {
            _logger = logger;
            _env = environment.EnvironmentName;
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<IActionResult> CreateProgramDetailCommand([FromForm] CreateProgramDetailCommand model)
        {
            _logger.LogInformation($"CreateApplicantCommand- Details", JsonConvert.SerializeObject(model));
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = await _mediator.Send(model);
                if (response.StatusCode == ResponseCode.BadRequest || response.StatusCode == ResponseCode.FAILED)
                {
                    return BadRequest(response);
                }
                else
                {
                    _logger.LogInformation($"CreateApplicantCommand, response ", JsonConvert.SerializeObject(response));

                    return Ok(response);

                }
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, _env);
            }
        }

        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateApplicantCommand([FromForm] UpdateApplicantCommand model)
        {
            _logger.LogInformation($"UpdateApplicantCommand - Details", JsonConvert.SerializeObject(model));
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = await _mediator.Send(model);
                if (response.StatusCode == ResponseCode.BadRequest || response.StatusCode == ResponseCode.FAILED)
                {
                    return BadRequest(response);
                }
                else if (response.StatusCode == ResponseCode.NOTFOUND)
                {
                    return NotFound(response);
                }
                else
                {
                    _logger.LogInformation($"UpdateApplicantCommand, response ", JsonConvert.SerializeObject(response));
                    return Ok(response);

                }

            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, _env);
            }
        }
    }
}
