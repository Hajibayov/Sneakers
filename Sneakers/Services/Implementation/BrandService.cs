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
    public class BrandService : IBrandService
    {

        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<SNEAKERS_BRAND> _brands;
        public BrandService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<SNEAKERS_BRAND> brands)
        {
            _context = context;
            _logger = logger;
            _brands = brands;
            _mapper = mapper;
        }

        public void AddBrand(BrandVM model, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                SNEAKERS_BRAND pos = _mapper.Map<SNEAKERS_BRAND>(model);
                _brands.Insert(pos);
                _brands.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB create position error";
                _logger.LogError($"PositionService CreatePosition : {traceId}" + $"{ex}");
            }
        }


    }
}
