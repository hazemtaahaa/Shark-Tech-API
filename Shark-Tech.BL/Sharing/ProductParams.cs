namespace Shark_Tech.BL;

public class ProductParams
{
    public string? Sort { get; set; }
    public Guid? CategoryId { get; set; }
    public int MaxPageSize { get; set; } = 10;
	private int _pageSize;
    public string?  Search { get; set; }
    public int PageSize
	{
		get { return _pageSize; }
		set { _pageSize = value>MaxPageSize?MaxPageSize:value; }
	}

	public int? PageNumber { get; set; } = 1;
}
