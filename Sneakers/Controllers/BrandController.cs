using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sneakers.DTO.HelperModels;
using Sneakers.DTO.HelperModels.Const;
using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModels.Main;
using Sneakers.Logging;
using Sneakers.Models;
using Sneakers.Services.Implementation;
using Sneakers.Services.Interface;
using Sneakers.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sneakers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public readonly IBrandService _brandService;
        private readonly ILoggerManager _logger;
        private readonly IValidation _validation;
        private readonly AppDbContext _context;

        public BrandController(IBrandService brandService, ILoggerManager logger, IValidation validation, AppDbContext context)
        {
            _brandService = brandService;
            _logger = logger;
            _validation = validation;
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<SNEAKERS_BRAND>>> GetAllBrands()
        {
            return Ok(await _context.SNEAKERS_BRAND.ToListAsync());
        }

        [HttpPost("add-brand")]
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
                    response.Status.Message = "Yeni brand yaradıldı.";
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


        [HttpPost("update_brand")]
        public IActionResult UpdateBrand([FromBody] BrandVM model, int id)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new Status();
            //     response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;


            int errorCode = 0;
            string message = null;


            try
            {
                _brandService.UpdateBrand(model, id, ref errorCode, ref message, response.TraceID);
                if (errorCode != 0)
                {
                    response.Status.ErrCode = errorCode;
                    response.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), response);
                }
                else
                {
                    response.Status.Message = "Dəyişikliklər uğurla həyata keçirildi.";
                }
            }
            catch (Exception ex)
            {
                response.Status.ErrCode = ErrorCode.SYSTEM;
                response.Status.Message = message;
                _logger.LogError($"PositionController UpdatePosition : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpDelete("delete_brand")]
        public IActionResult DeleteBrand(int id)
        {


            ResponseSimple response = new ResponseSimple();
            response.Status = new Status();
            response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;

            int errorCode = 0;
            string message = null;
            bool brandExists = false;


            try
            {
                _brandService.DeleteBrand(id, ref errorCode, ref brandExists, ref message, response.TraceID);
                if (errorCode != 0 || errorCode == 46)
                {
                    response.Status.ErrCode = errorCode;
                    response.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), response);
                }
                else
                {
                    if (brandExists == true)
                    {
                        response.Status.Message = message;
                    }
                    else
                    {
                        response.Status.Message = "Brand silindi.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status.ErrCode = ErrorCode.SYSTEM;
                response.Status.Message = message;
                _logger.LogError($"PositionController DeletePosition : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }
    }
}





