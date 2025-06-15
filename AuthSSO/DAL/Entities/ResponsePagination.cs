using Model.Entities;

namespace DAL.Entities;

public class ResponsePagination<T> 
    where T : class
{
    public int TotalEntity { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public List<T> Data { get; set; }
}
