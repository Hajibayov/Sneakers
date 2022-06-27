using Sneakers.DTO.RequestModel;

namespace Sneakers.Services.Interface
{
    public interface ISizeService
    {

        void AddSize(SizeVM size, ref int errorCode, ref string message, string traceId);
        void UpdateSize(SizeVM size, int id, ref int errorCode, ref string message, string traceId);

        void DeleteSize(int id, ref int errorCode, ref bool brandExists, ref string message, string traceId);
    }
}
