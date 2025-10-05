namespace ContactManager.Data.Entities;
public class PagingOptions
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public int Skip => (this.PageNumber - 1) * this.PageSize;
}