namespace Sneakers.Services.Interface
{
    public interface ISqlService
    {
        string Brands(bool isCount, bool isExport, int limit, int skip);
        string Types(bool isCount, bool isExport, int limit, int skip);
        string Sizes(bool isCount, bool isExport, int limit, int skip);

    }
}
