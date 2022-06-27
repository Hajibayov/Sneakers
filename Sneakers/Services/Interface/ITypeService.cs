using Sneakers.DTO.RequestModel;

namespace Sneakers.Services.Interface
{
    public interface ITypeService
    {
        void AddType(TypeVM type, ref int errorCode, ref string message, string traceId);
        void UpdateType(TypeVM type, int id, ref int errorCode, ref string message, string traceId);
        void DeleteType(int id, ref int errorCode, ref bool typeExists, ref string message, string traceId);
    }
}
