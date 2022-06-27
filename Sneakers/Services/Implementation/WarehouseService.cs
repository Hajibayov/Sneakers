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
    public class WarehouseService : IWarehouseService
    {
        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<WAREHOUSE> _wares;

        public WarehouseService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<WAREHOUSE> ware)
        {
            _context = context;
            _logger = logger;
            _wares = ware;
            _mapper = mapper;
        }

        public void AddWarehouse(WarehouseVM ware, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                WAREHOUSE war = _mapper.Map<WAREHOUSE>(ware);
                _wares.Insert(war);
                _wares.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB create size error";
                _logger.LogError($"PositionService CreatePosition : {traceId}" + $"{ex}");
            }

        }
        public void UpdateWarehouse(WarehouseVM ware, int id, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                WAREHOUSE oldData = _wares.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                WAREHOUSE newData = _mapper.Map<WAREHOUSE>(ware);
                newData.Id = id;
                oldData = newData;
                _wares.Update(oldData);
                _wares.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = ex.Message;
                _logger.LogError($"PositionService UpdatePosition : {traceId}" + $"{ex}");
            }
        }

        public void DeleteWarehouse(int id, ref int errorCode, ref bool typeExists, ref string message, string traceId)
        {

            WAREHOUSE ware = _wares.AllQuery.FirstOrDefault(n => n.Id == id);

            try
            {
                if (ware != null)
                {

                    _wares.Remove(ware);
                    _wares.Save();
                    message = "Bu anbar uğurla silindi.";

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
