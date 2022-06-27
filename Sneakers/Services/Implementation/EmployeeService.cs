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
    public class EmployeeService:IEmployeeService
    {
        private AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<EMPLOYEE> _employees;

        public EmployeeService(AppDbContext context, ILoggerManager logger, IMapper mapper, IRepository<EMPLOYEE> employees)
        {
            _context = context;
            _logger = logger;
            _employees = employees;
            _mapper = mapper;
        }

        public void AddEmployee(EmployeeVM employee, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                EMPLOYEE emp = _mapper.Map<EMPLOYEE>(employee);
                _employees.Insert(emp);
                _employees.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = "DB create brand error";
                _logger.LogError($"BrandService AddPosition : {traceId}" + $"{ex}");
            }
        }

        public void UpdateEmployee(EmployeeVM employee, int id, ref int errorCode, ref string message, string traceId)
        {
            try
            {
                EMPLOYEE oldData = _employees.AllQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
                EMPLOYEE newData = _mapper.Map<EMPLOYEE>(employee);
                newData.Id = id;
                oldData = newData;
                _employees.Update(oldData);
                _employees.Save();
            }
            catch (Exception ex)
            {
                errorCode = ErrorCode.DB;
                message = ex.Message;
                _logger.LogError($"PositionService UpdatePosition : {traceId}" + $"{ex}");
            }
        }

        public void DeleteEmployee(int id, ref int errorCode, ref bool brandExists, ref string message, string traceId)
        {

            EMPLOYEE employee = _employees.AllQuery.FirstOrDefault(n => n.Id == id);

            try
            {
                if (employee != null)
                {

                    _employees.Remove(employee);
                    _employees.Save();
                    message = "Bu işçi uğurla silindi.";

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
