public class PageResult<T>
{
    public List<T> Items { get; set; }
    public int totalPage { get; set; }
    public int itemFrom { get; set; }
    public int itemTo { get; set; }
    public int itemCount { get; set; }

    public PageResult(List<T> values, int totalCount, int pageSize, int pageNumber)
    {
        Items= values;
        itemCount=totalCount;
        itemFrom=pageSize*(pageNumber-1)+1;
        itemTo = itemFrom + pageSize - 1;
        totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
    
}