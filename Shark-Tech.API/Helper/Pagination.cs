namespace Shark_Tech.API;

public class Pagination<T> where T : class
{
    public Pagination(int? pageNumber, int pageSize, int totalCount, IReadOnlyList<T> data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        Data = data;
    }

    public int? PageNumber { get; set; } = 1;
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public IReadOnlyList<T>? Data { get; set; }
}
