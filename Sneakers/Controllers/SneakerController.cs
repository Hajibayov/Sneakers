using Microsoft.AspNetCore.Mvc;
using Sneakers.DTO.HelperModels;
using Sneakers.DTO.HelperModels.Const;
using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModels.Main;
using Sneakers.Logging;
using Sneakers.Models;
using Sneakers.Services.Interface;
using Sneakers.Validations;
using System;

namespace Sneakers.Controllers
{
    public class SneakerController : ControllerBase
    {
        public readonly ISneakersService _sneakersService;
        private readonly IValidation _validation;
        private readonly ILoggerManager _logger;

        public SneakerController(ISneakersService sneakersService, ILoggerManager logger, IValidation validation)
        {
            _sneakersService = sneakersService;
            _logger = logger;
            _validation = validation;
        }

        [HttpPost("add-sneakers")]
        public IActionResult AddSneakers([FromBody] SneakersVM sneaker)
        {
            ResponseSimple response = new ResponseSimple();
            //response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            response.Status = new Status();

            int errorCode = 0;
            string message = null;

            try
            {
                _sneakersService.AddSneakers(sneaker, ref errorCode, ref message, response.TraceID);
                if (errorCode != 0)
                {
                    response.Status.ErrCode = errorCode;
                    response.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), response);
                }
                else
                {
                    response.Status.Message = "Yeni ayaqqabı yaradıldı.";
                }
            }
            catch (Exception ex)
            {
                response.Status.ErrCode = ErrorCode.SYSTEM;
                response.Status.Message = message;
                _logger.LogError($"SizeController AddSize : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }
    }
}
