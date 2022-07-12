using Microsoft.AspNetCore.Mvc;
using Sneakers.DTO.HelperModels;
using Sneakers.DTO.HelperModels.Const;
using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModel.Inner;
using Sneakers.DTO.ResponseModels.Main;
using Sneakers.Logging;
using Sneakers.Models;
using Sneakers.Services.Interface;
using Sneakers.Validations;
using System;
using System.Diagnostics;

namespace Sneakers.Controllers
{
    public class SneakerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public readonly ISneakerService _sneakersService;
        private readonly IValidation _validation;
        private readonly ILoggerManager _logger;

        public SneakerController(ISneakerService sneakersService, ILoggerManager logger, IValidation validation, AppDbContext context)
        {
            _sneakersService = sneakersService;
            _logger = logger;
            _validation = validation;
            _context = context;

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
                _logger.LogError($"SneakerController AddSneaker : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpPost, Route("get-sneakers")]
        public IActionResult GetSneakers([FromBody] SNEAKER_FILTER_VIEW_MODEL model,int limit, int skip, bool isExport)
        {

            ResponseListTotal<SNEAKER_VIEW_MODEL> responseList = new ResponseListTotal<SNEAKER_VIEW_MODEL>();
            ResponseTotal<SNEAKER_VIEW_MODEL> response = new ResponseTotal<SNEAKER_VIEW_MODEL>();
            responseList.Response = response;
           // responseList.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            responseList.Status = new Status();
            int errorCode = 0;
            decimal totalCount = 0;
            string message = null;

            try
            {
                responseList.Response.Data = _sneakersService.GetSneakers(model,skip, limit, ref totalCount, isExport, ref errorCode, ref message, responseList.TraceID);
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
                _logger.LogError($"SneakerController GetSneakers : {responseList.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, responseList);
            }
        }


        [HttpGet, Route("get-sneaker")]
        public IActionResult GetSneaker(int id)
        {
    

            ResponseObject<SneakersVM> response = new ResponseObject<SneakersVM>();
            response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;
            response.Status = new Status();
            response.Response = new SneakersVM();

            int errorCode = 0;
            string message = null;

            try
            {
                response.Response = _sneakersService.GetSneaker(id, ref errorCode, ref message, response.TraceID);

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
                _logger.LogError($"SneakerController GetSneaker : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("update_sneaker")]
        public IActionResult UpdateSneaker([FromBody] SneakersVM sneaker, int id)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new Status();
            response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;


            int errorCode = 0;
            string message = null;


            try
            {
                _sneakersService.UpdateSneaker(sneaker, id, ref errorCode, ref message, response.TraceID);
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
                _logger.LogError($"SneakerController UpdateSneaker : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpDelete("delete_sneaker")]
        public IActionResult DeleteSneaker(int id)
        {


            ResponseSimple response = new ResponseSimple();
            response.Status = new Status();
            response.TraceID = Activity.Current.Id ?? HttpContext.TraceIdentifier;

            int errorCode = 0;
            string message = null;
           


            try
            {
                _sneakersService.DeleteSneaker(id, ref errorCode, ref message, response.TraceID);
                if (errorCode != 0 )
                {
                    response.Status.ErrCode = errorCode;
                    response.Status.Message = message;
                    return StatusCode(_validation.CheckErrorCode(errorCode), response);
                }
               
                    else
                    {
                        response.Status.Message = "Ayaqqabı silindi.";
                    }
                
            }
            catch (Exception ex)
            {
                response.Status.ErrCode = ErrorCode.SYSTEM;
                response.Status.Message = message;
                _logger.LogError($"SneakerController DeleteSneaker : {response.TraceID}" + $"{ex}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }


    }
}
