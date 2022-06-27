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
    public class SizeService : ISizeService
    {

        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SIZE> _sizes;

        public SizeService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SIZE> size)
        {
            _context = context;
            _logger = logger;
            _sizes = size;
            _mapper = mapper;
        }
        public void AddSize(SizeVM size, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SIZE siz = _mapper.Map<SIZE>(size);
                _sizes.Insert(siz);
                _sizes.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB create size error";
                _logger.LogError($"PositionService CreatePosition : {traceId}" + $"{ex}");
            }
        }

        public void UpdateSize(SizeVM size, int id, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SIZE oldData = _sizes.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                SIZE newData = _mapper.Map<SIZE>(size);
                newData.Id = id;
                oldData = newData;
                _sizes.Update(oldData);
                _sizes.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = ex.Message;
                _logger.LogError($"PositionService UpdatePosition : {traceId}" + $"{ex}");
            }
        }

        public void DeleteSize(int id, ref int errorCode, ref bool sizeExists, ref string message, string traceId)
        {

            SIZE size = _sizes.AllQuery.FirstOrDefault(n => n.Id == id);

            try
            {
                if (size != null)
                {

                    _sizes.Remove(size);
                    _sizes.Save();
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
                _logger.LogError($"PositionService DeletePosition : {traceId}" + $"{ex}");
            }
        }
    }
}
