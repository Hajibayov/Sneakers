using Sneakers.DTO.RequestModel;

namespace Sneakers.Services.Interface
{
    public interface IWarehouseService
    {

        void AddWarehouse(WarehouseVM ware, ref int errorCode, ref string message, string traceId);
        void UpdateWarehouse(WarehouseVM ware, int id, ref int errorCode, ref string message, string traceId);
        void DeleteWarehouse(int id, ref int errorCode, ref bool typeExists, ref string message, string traceId);
    }
}
