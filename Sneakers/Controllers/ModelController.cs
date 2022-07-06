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
    public class ModelController : ControllerBase    
    {
        AppConfiguration config = new AppConfiguration();
        public readonly IModelService _modelService;
        private readonly ILoggerManager _logger;
        private readonly IValidation _validation;

        public ModelController(IModelService modelService, ILoggerManager logger, IValidation validation)
        {
            _modelService = modelService;
            _logger = logger;
            _validation = validation;
        }

        [HttpGet, Route("get-models")]
        public IActionResult GetModels(int limit, int skip, bool isExport)
        {


            ResponseListTotal<MODEL_VIEW_MODEL> responseList = new ResponseListTotal<MODEL_VIEW_MODEL>();
            ResponseTotal<MODEL_VIEW_MODEL> response = new ResponseTotal<MODEL_VIEW_MODEL>();

            responseList.Response = response;
            responseList.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            responseList.Status = new Status();
            int errorCode = 0;
            decimal totalCount = 0;
            string message = null;

            try
            {
                responseList.Response.Data = _modelService.GetModels(skip, limit, ref totalCount, isExport, ref errorCode, ref message, responseList.TraceID);
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
                _logger.LogError($"ModelController Models : {responseList.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, responseList);
            }
        }

        [HttpGet, Route("get-model")]
        public IActionResult GetModel(int id)
        {

            ResponseObject<ModelVM> response = new ResponseObject<ModelVM>();
            response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            response.Status = new Status();
            response.Response = new ModelVM();

            int errorCode = 0;
            string message = null;

            try
            {
                response.Response = _modelService.GetModel(id, ref errorCode, ref message, response.TraceID);

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
                _logger.LogError($"ModelController GetModel : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPost("add-model")]
        public IActionResult AddModel([FromBody] ModelVM model)
        {
            ResponseSimple response = new ResponseSimple();
            //response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            response.Status = new Status();

            int errorCode = 0;
            string message = null;

            try
            {
                _modelService.AddModel(model, ref errorCode, ref message, response.TraceID);
                if (errorCode != 0)
                {
                    response.Status.ErrCode = errorCode;
                    response.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), response);
                }
                else
                {
                    response.Status.Message = "Yeni model yaradıldı.";
                }
            }
            catch (Exception ex)
            {
                response.Status.ErrCode = ErrorCode.SYSTEM;
                response.Status.Message = message;
                _logger.LogError($"ModelController AddModel : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpPost("update_model")]
        public IActionResult UpdateModel([FromBody] ModelVM model, int id)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new Status();
            //     response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;


            int errorCode = 0;
            string message = null;


            try
            {
                _modelService.UpdateModel(model, id, ref errorCode, ref message, response.TraceID);
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

        [HttpDelete("delete_model")]
        public IActionResult DeleteModel(int id)
        {


            ResponseSimple response = new ResponseSimple();
            response.Status = new Status();
          //  response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;

            int errorCode = 0;
            string message = null;
            bool brandExists = false;


            try
            {
                _modelService.DeleteModel(id, ref errorCode, ref brandExists, ref message, response.TraceID);
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
                        response.Status.Message = "Model silindi.";
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
