using CapitalPlacementTaskAPI.Domain.BindingModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CapitalPlacementTaskAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly string _env;
        public BaseController()
        { }

        public BaseController(IWebHostEnvironment env)
        {
            _env = env.EnvironmentName;
        }

        internal IActionResult HandleException(Exception ex, ILogger logger, string env)
        {
            logger.LogError(ex, ex.Message);

            if (env != null && (env == "local" || env.ToLower() == "development" || env == "uat"))
            {
                return StatusCode(500, new ServiceResponse()
                {
                    StatusCode = Domain.Const.ResponseCode.Error,
                    StatusMessage = ex.Message
                });
            }
            else
            {
                return StatusCode(500, new ServiceResponse()
                {
                    StatusCode = Domain.Const.ResponseCode.Error,
                    StatusMessage = "Something went wrong. It’s not you, it’s us. Please give it another try."
                });
            }
        }
    }
}
