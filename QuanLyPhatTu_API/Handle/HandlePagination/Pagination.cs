namespace QuanLyPhatTu_API.Handle.HandlePagination
{
    public class Pagination
    {
        public static PageResult<T> GetPageData<T>(IQueryable<T>data, int pageSize, int pageNumber)
        {
            int totalItems = data.Count();
            int totalPages = (int)Math.Ceiling((decimal)totalItems/ pageSize);
            var pageData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new PageResult<T>(pageData, totalItems, totalPages, pageNumber, pageSize);
        }
    }
}
