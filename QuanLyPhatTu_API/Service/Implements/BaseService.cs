using QuanLyPhatTu_API.DataContext;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class BaseService
    {
        public readonly AppDbContext _context;
        public BaseService()
        {
            _context = new AppDbContext();
        }
    }
}
