using Microsoft.AspNetCore.Mvc;
using Sneakers.DTO.HelperModels;
using Sneakers.DTO.HelperModels.Const;
using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModel.Inner;
using Sneakers.DTO.ResponseModels.Main;
using Sneakers.Logging;
using Sneakers.Services.Interface;
using Sneakers.Validations;
using System;
using System.Diagnostics;

namespace Sneakers.Controllers
{
    public class TypeController : ControllerBase
    {
        public readonly ITypeService _typeService;
        private readonly ILoggerManager _logger;
        private readonly IValidation _validation;

        public TypeController(ITypeService typeService, ILoggerManager logger, IValidation validation)
        {
            _typeService = typeService;
            _logger = logger;
            _validation = validation;
        }

        [HttpGet, Route("get-types")]
        public IActionResult GetTypes(int limit, int skip, bool isExport)
        {

            ResponseListTotal<TYPE_VIEW_MODEL> responseList = new ResponseListTotal<TYPE_VIEW_MODEL>();
            ResponseTotal<TYPE_VIEW_MODEL> response = new ResponseTotal<TYPE_VIEW_MODEL>();


            responseList.Response = response;
            responseList.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            responseList.Status = new Status();
            int errorCode = 0;
            decimal totalCount = 0;
            string message = null;

            try
            {
                responseList.Response.Data = _typeService.GetTypes(skip, limit, ref totalCount, isExport, ref errorCode, ref message, responseList.TraceID);
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
                _logger.LogError($"TypeController GetTypes : {responseList.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, responseList);
            }
        }

        [HttpGet, Route("get-type")]
        public IActionResult GetType(int id)
        {

            ResponseObject<TypeVM> response = new ResponseObject<TypeVM>();
            response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            response.Status = new Status();
            response.Response = new TypeVM();

            int errorCode = 0;
            string message = null;

            try
            {
                response.Response = _typeService.GetType(id, ref errorCode, ref message, response.TraceID);

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
                _logger.LogError($"TypeController GetType : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("add-type")]
        public IActionResult AddSize([FromBody] TypeVM type)
        {
            ResponseSimple response = new ResponseSimple();
            //response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            response.Status = new Status();

            int errorCode = 0;
            string message = null;

            try
            {
                _typeService.AddType(type, ref errorCode, ref message, response.TraceID);
                if (errorCode != 0)
                {
                    response.Status.ErrCode = errorCode;
                    response.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), response);
                }
                else
                {
                    response.Status.Message = "Yeni type yaradıldı.";
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
        [HttpPost("update_type")]
        public IActionResult UpdateType([FromBody] TypeVM type, int id)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new Status();
            //     response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;


            int errorCode = 0;
            string message = null;


            try
            {
                _typeService.UpdateType(type, id, ref errorCode, ref message, response.TraceID);
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

                //?
                _logger.LogError($"SizeController UpdateSize : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpDelete("delete_type")]
        public IActionResult DeleteSize(int id)
        {


            ResponseSimple response = new ResponseSimple();
            response.Status = new Status();
            response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;

            int errorCode = 0;
            string message = null;
            bool typeExists = false;


            try
            {
                _typeService.DeleteType(id, ref errorCode, ref typeExists, ref message, response.TraceID);
                if (errorCode != 0 || errorCode == 46)
                {
                    response.Status.ErrCode = errorCode;
                    response.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), response);
                }
                else
                {
                    if (typeExists == true)
                    {
                        response.Status.Message = message;
                    }
                    else
                    {
                        response.Status.Message = "Type silindi.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status.ErrCode = ErrorCode.SYSTEM;
                response.Status.Message = message;
                _logger.LogError($"SizeController DeleteSize : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }
    }
}
