using Sneakers.DTO.RequestModel;

namespace Sneakers.Services.Interface
{
    public interface IEmployeeService
    {
        void AddEmployee(EmployeeVM employee, ref int errorCode, ref string message, string traceId);
        void UpdateEmployee(EmployeeVM employee, int id, ref int errorCode, ref string message, string traceId);
        void DeleteEmployee(int id, ref int errorCode, ref bool brandExists, ref string message, string traceId);
    }
}
