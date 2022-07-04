namespace Sneakers.Services.Interface
{
    public interface ISqlService
    {
        string Brands(bool isCount, bool isExport, int limit, int skip);
    }
}
