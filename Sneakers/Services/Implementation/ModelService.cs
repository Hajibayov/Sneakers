

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sneakers.DTO.HelperModels.Const;
using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModel.Inner;
using Sneakers.Infrastructure.Repository;
using Sneakers.Logging;
using Sneakers.Models;
using Sneakers.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sneakers.Services.Implementation
{
    public class ModelService : IModelService
    {
        AppConfiguration config = new AppConfiguration();
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SNEAKERS_MODEL> _models;
        private readonly ISqlService _sqlService;


        public ModelService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SNEAKERS_MODEL> model, ISqlService sqlService)
        {
            _logger = logger;
            _models = model;
            _mapper = mapper;
            _sqlService = sqlService;

        }
        public List<MODEL_VIEW_MODEL> GetModels(int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string traceId)
        {
            List<MODEL_VIEW_MODEL> response = new List<MODEL_VIEW_MODEL>();

            try
            {
                using (SqlConnection con = new SqlConnection(config.ConnectionString))
                {
                    SqlCommand cmd;
                    using (cmd = con.CreateCommand())
                    {
                        con.Open();

                        cmd.CommandText = _sqlService.Models(false, isExport, limit, skip);
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            MODEL_VIEW_MODEL model_view_model = new MODEL_VIEW_MODEL()
                            {
                                Id = (int)rdr["Id"],
                                Model = rdr["Model"].ToString(),

                            };
                            response.Add(model_view_model);
                        }
                        rdr.Close();
                        cmd = con.CreateCommand();
                        if (!isExport)
                        {
                            cmd.CommandText = _sqlService.Models(true, false, limit, skip);

                            rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                totalCount = (int)rdr["totalCount"];
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB get models error";
                _logger.LogError($"ModelService GetModels : {traceId}" + $"{ex}");
            }
            return response;
        }

        public ModelVM GetModel(int id, ref int errorCode, ref string message, string traceId)
        {
            ModelVM result = new ModelVM();
            try
            {
                SNEAKERS_MODEL model = _models.AllQuery.FirstOrDefault(x => x.Id == id);
                result = _mapper.Map<ModelVM>(model);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB get model error";
                _logger.LogError($"ModelService GetModel : {traceId}" + $"{ex}");
            }
            return result;

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
