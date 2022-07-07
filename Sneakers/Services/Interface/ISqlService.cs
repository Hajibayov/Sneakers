using Sneakers.DTO.ResponseModel.Inner;

namespace Sneakers.Services.Interface
{
    public interface ISqlService
    {
        string Brands(bool isCount, bool isExport, int limit, int skip);
        string Models(bool isCount, bool isExport, int limit, int skip);

        string Types(bool isCount, bool isExport, int limit, int skip);
        string Sizes(bool isCount, bool isExport, int limit, int skip);

        string Sneakers(bool isCount, bool isExport, int limit, int skip, SNEAKER_FILTER_VIEW_MODEL model);

    }
}
