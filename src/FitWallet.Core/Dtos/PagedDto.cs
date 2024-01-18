namespace FitWallet.Core.Dtos;

public class PagedDto<TData>
{
    public List<TData> Data { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public bool IsNext { get; set; }
}