using AutoMapper;
using Sneakers.DTO.HelperModels.Const;
using Sneakers.DTO.RequestModel;
using Sneakers.Infrastructure.Repository;
using Sneakers.Logging;
using Sneakers.Models;
using Sneakers.Services.Interface;
using System;


namespace Sneakers.Services.Implementation
{
    public class SneakersService:ISneakersService
    {

        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SNEAKERS> _sneakers;

        public SneakersService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SNEAKERS> sneaker)
        {
            _context = context;
            _logger = logger;
            _sneakers = sneaker;
            _mapper = mapper;
        }

        public void AddSneakers(SneakersVM sneaker, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS snk = _mapper.Map<SNEAKERS>(sneaker);
              // snk.CreatedAt = DateTime.Now;
                snk.UpdatedBy = null;
                snk.UpdatedAt = null;
                _sneakers.Insert(snk);
                _sneakers.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB create size error";
                _logger.LogError($"PositionService CreatePosition : {traceId}" + $"{ex}");
            }
        }
    }
}
