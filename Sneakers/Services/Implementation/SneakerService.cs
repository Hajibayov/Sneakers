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
    public class SneakerService : ISneakerService
    {
        AppConfiguration config = new AppConfiguration();
        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SNEAKERS> _sneakers;
        private readonly ISqlService _sqlService;

        public SneakerService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SNEAKERS> sneaker, ISqlService sqlService)
        {
            _context = context;
            _logger = logger;
            _sneakers = sneaker;
            _mapper = mapper;
            _sqlService = sqlService;

        }

        public void AddSneakers(SneakersVM sneaker, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS snk = _mapper.Map<SNEAKERS>(sneaker);
                snk.CreatedAt = DateTime.Now;
                snk.UpdatedBy = null;
                snk.UpdatedAt = null;
                _sneakers.Insert(snk);
                _sneakers.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB create sneaker error";
                _logger.LogError($"SneakerService CreateSneaker : {traceId}" + $"{ex}");
            }
        }
        public SneakersVM GetSneaker(int id, ref int errorCode, ref string message, string traceId)
        {
            SneakersVM result = new SneakersVM();
            try
            {
                SNEAKERS sneaker = _sneakers.AllQuery.FirstOrDefault(x => x.Id == id);
                result = _mapper.Map<SneakersVM>(sneaker);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB get sneaker error";
                _logger.LogError($"SneakerService GetSneaker : {traceId}" + $"{ex}");
            }
            return result;
        }
        public List<SNEAKER_VIEW_MODEL> GetSneakers(SNEAKER_FILTER_VIEW_MODEL model, int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string traceId)
        {
            List<SNEAKER_VIEW_MODEL> response = new List<SNEAKER_VIEW_MODEL>();
            try
            {
                using (SqlConnection con = new SqlConnection(config.ConnectionString))
                {
                    SqlCommand cmd;
                    using (cmd = con.CreateCommand())
                    {
                        con.Open();
                        cmd.CommandText = _sqlService.Sneakers(false, isExport, limit, skip, model);

                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            SNEAKER_VIEW_MODEL a = new SNEAKER_VIEW_MODEL()
                            {
                                Id = (int)rdr["ID"],
                                Brand = rdr["BRAND"].ToString(),
                                Model = rdr["MODEL"].ToString(),
                                Type = rdr["TYPE"].ToString(),
                                Price = rdr["PRICE"].ToString(),
                            };
                            response.Add(a);
                        }
                        rdr.Close();
                        cmd = con.CreateCommand();
                        if (!isExport)
                        {
                            cmd.CommandText = _sqlService.Sneakers(true, false, limit, skip, model);

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
                message = ex.Message;
                _logger.LogError($"SneakerService GetSneakers : {traceId}" + $"{ex}");
            }

            return response;
        }

        public void UpdateSneaker(SneakersVM sneaker, int id, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS oldData = _sneakers.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                SNEAKERS newData = _mapper.Map<SNEAKERS>(sneaker);
                newData.Id = id;
                newData.CreatedAt = oldData.CreatedAt;
                newData.UpdatedAt = DateTime.Now;
                newData.CreatedBy = oldData.CreatedBy;
                _sneakers.Update(newData);
                _sneakers.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB update sneaker error";
                _logger.LogError($"SneakerController CreateSneaker : {traceId}" + $"{ex}");
            }
        }

        public void DeleteSneaker(int id, ref int errorCode, ref string message, string traceId)
        {

            SNEAKERS sneaker = _sneakers.AllQuery.FirstOrDefault(n => n.Id == id);

            try
            {
                if (sneaker != null)
                {

                    _sneakers.Remove(sneaker);
                    _sneakers.Save();
                    message = "Bu ayaqqabı uğurla silindi.";

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
