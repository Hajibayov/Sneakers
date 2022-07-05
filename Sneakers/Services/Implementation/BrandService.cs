using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sneakers.DTO.HelperModels.Const;
using Sneakers.DTO.RequestModel;
using Sneakers.Infrastructure.Repository;
using Sneakers.Logging;
using Sneakers.Models;
using Sneakers.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TeamControlV2.DTO.ResponseModels.Inner;

namespace Sneakers.Services.Implementation
{
    public class BrandService : IBrandService
    {
        AppConfiguration config = new AppConfiguration();
        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SNEAKERS_BRAND> _brands;
        private readonly ISqlService _sqlService;


        public BrandService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SNEAKERS_BRAND> brands, ISqlService sqlService)
        {
            _context = context;
            _logger = logger;
            _brands = brands;
            _mapper = mapper;
            _sqlService = sqlService;

        }

        public void AddBrand(BrandVM model, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS_BRAND brd = _mapper.Map<SNEAKERS_BRAND>(model);
                _brands.Insert(brd);
                _brands.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB create brand error";
                _logger.LogError($"BrandService AddBrand : {traceId}" + $"{ex}");
            }
        }

        public List<BRAND_VIEW_MODEL> GetBrands(int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string traceId)
        {
            List<BRAND_VIEW_MODEL> response = new List<BRAND_VIEW_MODEL>();

            try
            {
                using (SqlConnection con = new SqlConnection(config.ConnectionString))
                {
                    SqlCommand cmd;
                    using (cmd = con.CreateCommand())
                    {
                        con.Open();

                        cmd.CommandText = _sqlService.Brands(false, isExport, limit, skip);
                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            BRAND_VIEW_MODEL brand_view_model = new BRAND_VIEW_MODEL()
                            {
                                Id = (int)rdr["Id"],
                                Brand = rdr["Brand"].ToString(),
                               
                            };
                            response.Add(brand_view_model);
                        }
                        rdr.Close();
                        cmd = con.CreateCommand();
                        if (!isExport)
                        {
                            cmd.CommandText = _sqlService.Brands(true, false, limit, skip);

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

        public BrandVM GetBrand(int id, ref int errorCode, ref string message, string traceId)
        {
            BrandVM result = new BrandVM();
            try
            {
                SNEAKERS_BRAND brand = _brands.AllQuery.FirstOrDefault(x => x.Id == id);
                result = _mapper.Map<BrandVM>(brand);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB get brand error";
                _logger.LogError($"BrandService GetBrand : {traceId}" + $"{ex}");
            }
            return result;

        }
            public void UpdateBrand(BrandVM model, int id, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS_BRAND oldData = _brands.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                SNEAKERS_BRAND newData = _mapper.Map<SNEAKERS_BRAND>(model);
                newData.Id = id;
                oldData = newData;
                _brands.Update(oldData);
                _brands.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = ex.Message;
                _logger.LogError($"PositionService UpdatePosition : {traceId}" + $"{ex}");
            }
        }

        public void DeleteBrand(int id, ref int errorCode, ref bool brandExists, ref string message, string traceId)
        {

            SNEAKERS_BRAND brand = _brands.AllQuery.FirstOrDefault(n => n.Id == id);

            try
            {
                if (brand != null)
                {

                    _brands.Remove(brand);
                    _brands.Save();
                    message = "Bu brand uğurla silindi.";

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
