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
    public class SizeService : ISizeService
    {
        AppConfiguration config = new AppConfiguration();
        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SIZE> _sizes;
        private readonly ISqlService _sqlService;


        public SizeService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SIZE> size, ISqlService sqlService)
        {
            _context = context;
            _logger = logger;
            _sizes = size;
            _mapper = mapper;
            _sqlService = sqlService;
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

        public List<SIZE_VIEW_MODEL> GetSizes(int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string traceId)
        {
            List<SIZE_VIEW_MODEL> response = new List<SIZE_VIEW_MODEL>();

            try
            {
                using (SqlConnection con = new SqlConnection(config.ConnectionString))
                {
                    SqlCommand cmd;
                    using (cmd = con.CreateCommand())
                    {
                        con.Open();

                        cmd.CommandText = _sqlService.Sizes(false, isExport, limit, skip);
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            SIZE_VIEW_MODEL size_view_model = new SIZE_VIEW_MODEL()
                            {
                                Id = (int)rdr["Id"],
                                Size = rdr["Size"].ToString(),

                            };
                            response.Add(size_view_model);
                        }
                        rdr.Close();
                        cmd = con.CreateCommand();
                        if (!isExport)
                        {
                            cmd.CommandText = _sqlService.Sizes(true, false, limit, skip);

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
                message = "DB get brands error";
                _logger.LogError($"BrandService GetBrands : {traceId}" + $"{ex}");
            }
            return response;
        }

        public SizeVM GetSize(int id, ref int errorCode, ref string message, string traceId)
        {
            SizeVM result = new SizeVM();
            try
            {
                SIZE size = _sizes.AllQuery.FirstOrDefault(x => x.Id == id);
                result = _mapper.Map<SizeVM>(size);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB get size error";
                _logger.LogError($"SizeService GetSize : {traceId}" + $"{ex}");
            }
            return result;

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
