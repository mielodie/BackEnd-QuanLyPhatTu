namespace QuanLyPhatTu_API.DataContext
{
    public class Settings
    {
        public static string MyConnectString()
        {
            return "server=QUANMOVIT\\SQLEXPRESS; database=quanlyphatut_api; integrated security=sspi; encrypt=true; trustservercertificate = true;";
        }
    }
}
