using CapitalPlacementTaskAPI.Domain.BindingModels;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnboardingController : BaseController
    {
        private readonly ILogger<OnboardingController> _logger;
        private readonly string _env;
        private readonly IMediator _mediator;
        public OnboardingController(ILogger<OnboardingController> logger, IWebHostEnvironment environment, IMediator mediator)
        {
            _logger = logger;
            _env = environment.EnvironmentName;
            _mediator = mediator;
        }

    }
}
