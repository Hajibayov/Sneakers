

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
    public class ModelService : IModelService
    {
        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SNEAKERS_MODEL> _models;


        public ModelService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SNEAKERS_MODEL> model)
        {
            _context = context;
            _logger = logger;
            _models = model;
            _mapper = mapper;

        }
        public void AddModel(ModelVM model, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS_MODEL mod = _mapper.Map<SNEAKERS_MODEL>(model);
                _models.Insert(mod);
                _models.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB create size error";
                _logger.LogError($"PositionService CreatePosition : {traceId}" + $"{ex}");
            }
        }

        public void UpdateModel(ModelVM model, int id, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS_MODEL oldData = _models.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                SNEAKERS_MODEL newData = _mapper.Map<SNEAKERS_MODEL>(model);
                newData.Id = id;
                oldData = newData;
                _models.Update(oldData);
                _models.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = ex.Message;
                _logger.LogError($"PositionService UpdatePosition : {traceId}" + $"{ex}");
            }
        }
        public void DeleteModel(int id, ref int errorCode, ref bool modelExists, ref string message, string traceId)
        {

            SNEAKERS_MODEL model = _models.AllQuery.FirstOrDefault(n => n.Id == id);

            try
            {
                if (model != null)
                {

                    _models.Remove(model);
                    _models.Save();
                    message = "Bu model uğurla silindi.";

                }
                else
                {
                    message = "Belə model yoxdu.";
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
