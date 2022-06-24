using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sneakers.DTO.HelperModels;
using Sneakers.DTO.HelperModels.Const;
using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModels.Main;
using Sneakers.Logging;
using Sneakers.Services.Implementation;
using Sneakers.Services.Interface;
using Sneakers.Validations;
using System;
using System.Diagnostics;

namespace Sneakers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public  readonly IBrandService _brandService;
        private readonly ILoggerManager _logger;
        private readonly IValidation _validation;

        public BrandController(IBrandService brandService, ILoggerManager logger, IValidation validation)
        {
            _brandService = brandService;
            _logger = logger;
            _validation = validation;
        }

        [HttpPost ("add-brand")]
        public IActionResult AddBrand([FromBody] BrandVM model)
        {
            ResponseSimple response = new ResponseSimple();
            //response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            response.Status = new Status();

            int errorCode = 0;
            string message = null;

            try
            {
                _brandService.AddBrand(model, ref errorCode, ref message, response.TraceID);
                if (errorCode != 0)
                {
                    response.Status.ErrCode = errorCode;
                    response.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), response);
                }
                else
                {
                    response.Status.Message = "Yeni vəzifə yaradıldı.";
                }
            }
            catch (Exception ex)
            {
                response.Status.ErrCode = ErrorCode.SYSTEM;
                response.Status.Message = message;
                _logger.LogError($"BrandController AddBrand : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }
    }
}
