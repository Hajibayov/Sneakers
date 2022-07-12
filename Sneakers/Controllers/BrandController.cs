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
using TeamControlV2.DTO.ResponseModels.Inner;

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

        [HttpGet, Route("get-brands")]
        public IActionResult GetBrands(int limit, int skip, bool isExport)
        {
           

            ResponseListTotal<BRAND_VIEW_MODEL> responseList = new ResponseListTotal<BRAND_VIEW_MODEL>();
            ResponseTotal<BRAND_VIEW_MODEL> response = new ResponseTotal<BRAND_VIEW_MODEL>();

            responseList.Response = response;
            responseList.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            responseList.Status = new Status();
            int errorCode = 0;
            decimal totalCount = 0;
            string message = null;

            try
            {
                responseList.Response.Data = _brandService.GetBrands(skip, limit, ref totalCount, isExport, ref errorCode, ref message, responseList.TraceID);
                responseList.Response.Total = totalCount;
                if (errorCode != 0)
                {
                    responseList.Status.ErrCode = errorCode;
                    responseList.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), responseList);
                }
                else
                {
                    return Ok(responseList);
                }
            }
            catch (Exception ex)
            {
                responseList.Status.ErrCode = ErrorCode.SYSTEM;
                responseList.Status.Message = message;
                _logger.LogError($"BrandController GetBrands : {responseList.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, responseList);
            }
        }

        [HttpGet, Route("get-brand")]
        public IActionResult GetBrand(int id)
        {
        
            ResponseObject<BrandVM> response = new ResponseObject<BrandVM>();
            response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            response.Status = new Status();
            response.Response = new BrandVM();

            int errorCode = 0;
            string message = null;

            try
            {
                response.Response = _brandService.GetBrand(id, ref errorCode, ref message, response.TraceID);

                if (errorCode != 0)
                {
                    response.Status.ErrCode = errorCode;
                    response.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Status.ErrCode = ErrorCode.SYSTEM;
                response.Status.Message = message;
                _logger.LogError($"BrandController GetBrand : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
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
                    response.Status.Message = "Yeni brend yaradıldı.";
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
                        response.Status.Message = "Brend silindi.";
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





