using Sneakers.DTO.RequestModel;

namespace Sneakers.Services.Interface
{
    public interface IModelService
    {
        void AddModel(ModelVM model, ref int errorCode, ref string message, string traceId);
        void UpdateModel(ModelVM model, int id, ref int errorCode, ref string message, string traceId);

        void DeleteModel(int id, ref int errorCode, ref bool modelExists, ref string message, string traceId);
    }
}
