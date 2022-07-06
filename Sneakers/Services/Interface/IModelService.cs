using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModel.Inner;
using System.Collections.Generic;

namespace Sneakers.Services.Interface
{
    public interface IModelService
    {
        List<MODEL_VIEW_MODEL> GetModels(int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string traceId);

        ModelVM GetModel(int id, ref int errorCode, ref string message, string traceId);

        void AddModel(ModelVM model, ref int errorCode, ref string message, string traceId);
        void UpdateModel(ModelVM model, int id, ref int errorCode, ref string message, string traceId);

        void DeleteModel(int id, ref int errorCode, ref bool modelExists, ref string message, string traceId);
    }
}
