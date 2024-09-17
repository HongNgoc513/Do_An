using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DoAn1.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        public RegisterModel(ILogger<RegisterModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public string tendangnhap { get; set; }
        [BindProperty]
        public string matkhau { get; set; }
        public void OnGet()
        {
            TempData["Message"] = "";
        }
        public IActionResult OnPost()
        {
            if (!CheckAccountExists(tendangnhap))
            {
                InsertUser(tendangnhap, matkhau);
                return RedirectToPage("/Login");
            }
            else
            {
                return Page();
            }

        }
        public void InsertUser(string tendangnhap, string matkhau)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnect.Conn))
            {
                connection.Open();
                string query = "INSERT INTO Users (Tendangnhap, Matkhau, Quyenhan) " +
                               "VALUES (@TenDangNhap, @MatKhau, @QuyenHan)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenDangNhap", tendangnhap);
                    command.Parameters.AddWithValue("@MatKhau", matkhau);
                    command.Parameters.AddWithValue("@QuyenHan", "KhachHang");

                    command.ExecuteNonQuery(); // Thực thi truy vấn để thêm người dùng mới
                }
            }
        }

        private bool CheckAccountExists(string username)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnect.Conn))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM Users WHERE Tendangnhap = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
