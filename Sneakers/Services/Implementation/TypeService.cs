using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sneakers.DTO.HelperModels.Const;
using Sneakers.DTO.RequestModel;
using Sneakers.Infrastructure.Repository;
using Sneakers.Logging;
using Sneakers.Models;
using Sneakers.Services.Interface;
using System;
using System.Linq;

namespace Sneakers.Services.Implementation
{
    public class TypeService:ITypeService
    {
        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SNEAKERS_TYPE> _types;


        public TypeService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SNEAKERS_TYPE> type)
        {
            _context = context;
            _logger = logger;
            _types = type;
            _mapper = mapper;
        }
        public void AddType(TypeVM type, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS_TYPE typ = _mapper.Map<SNEAKERS_TYPE>(type);
                _types.Insert(typ);
                _types.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB create size error";
                _logger.LogError($"PositionService CreatePosition : {traceId}" + $"{ex}");
            }

        }
        public void UpdateType(TypeVM type, int id, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS_TYPE oldData = _types.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                SNEAKERS_TYPE newData = _mapper.Map<SNEAKERS_TYPE>(type);
                newData.Id = id;
                oldData = newData;
                _types.Update(oldData);
                _types.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = ex.Message;
                _logger.LogError($"PositionService UpdatePosition : {traceId}" + $"{ex}");
            }
        }
        public void DeleteType(int id, ref int errorCode, ref bool typeExists, ref string message, string traceId)
        {

            SNEAKERS_TYPE type = _types.AllQuery.FirstOrDefault(n => n.Id == id);

            try
            {
                if (type != null)
                {

                    _types.Remove(type);
                    _types.Save();
                    message = "Bu size uğurla silindi.";

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB delete position error";
                _logger.LogError($"TypeService TypePosition : {traceId}" + $"{ex}");
            }
        }
    }
}
