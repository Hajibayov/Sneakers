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
    public class TypeService:ITypeService
    {
        AppConfiguration config = new AppConfiguration();
        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SNEAKERS_TYPE> _types;
        private readonly ISqlService _sqlService;


        public TypeService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SNEAKERS_TYPE> types, ISqlService sqlService)
        {
            _context = context;
            _logger = logger;
            _types = types;
            _mapper = mapper;
            _sqlService = sqlService;
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
                message = "DB create type error";
                _logger.LogError($"TypeService CreateType : {traceId}" + $"{ex}");
            }

        }

        public List<TYPE_VIEW_MODEL> GetTypes(int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string traceId)
        {
            List<TYPE_VIEW_MODEL> response = new List<TYPE_VIEW_MODEL>();

            try
            {
                using (SqlConnection con = new SqlConnection(config.ConnectionString))
                {
                    SqlCommand cmd;
                    using (cmd = con.CreateCommand())
                    {
                        con.Open();

                        cmd.CommandText = _sqlService.Types(false, isExport, limit, skip);
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            TYPE_VIEW_MODEL type_view_model = new TYPE_VIEW_MODEL()
                            {
                                Id = (int)rdr["Id"],
                                Type = rdr["Type"].ToString(),

                            };
                            response.Add(type_view_model);
                        }
                        rdr.Close();
                        cmd = con.CreateCommand();
                        if (!isExport)
                        {
                            cmd.CommandText = _sqlService.Types(true, false, limit, skip);

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
                message = "DB get types error";
                _logger.LogError($"TypeService GetTypes : {traceId}" + $"{ex}");
            }
            return response;
        }

        public TypeVM GetType(int id, ref int errorCode, ref string message, string traceId)
        {
            TypeVM result = new TypeVM();
            try
            {
                SNEAKERS_TYPE type = _types.AllQuery.FirstOrDefault(x => x.Id == id);
                result = _mapper.Map<TypeVM>(type);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB get type error";
                _logger.LogError($"TypeService GetType : {traceId}" + $"{ex}");
            }
            return result;

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
