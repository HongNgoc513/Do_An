using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace DoAn1.Pages
{
    public class SonModel : PageModel
    {
        public string productID { get; set; }
        public string Gia { get; set; } 
        public DataTable Son { get; set; }
        public void TimKiem1(string query)
        {
            using (SqlConnection con = new SqlConnection(SQLConnect.Conn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    Son = new DataTable();
                    adapter.Fill(Son);
                }
            }
        }
        public void OnGet()
        {
            productID = "";
            productID = Request.Query["ProductID"];
            Gia = Request.Query["Gia"];
            if (productID != null)
            {
                using (SqlConnection con = new SqlConnection(SQLConnect.Conn))
                {
                    con.Open();
                    string query1 = @"INSERT INTO GioHang (Tendangnhap, Masanpham, Soluong, Gia) VALUES (@Tendangnhap, @Masanpham, 1, @Gia)";
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        cmd.Parameters.AddWithValue("@Tendangnhap", User.Identity.Name);
                        cmd.Parameters.AddWithValue("@Masanpham", productID);
                        cmd.Parameters.AddWithValue("@Gia", int.Parse(Gia));
                        Console.WriteLine(productID);
                        // Thực thi truy vấn SQL
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine(1);
                        }
                        else
                        {
                        }
                    }
                }
            }
            if (Son != null)
            {
                Son.Clear();
            }    
            string query = @"SELECT * FROM SanPham INNER JOIN LoaiHang ON SanPham.Mahang = LoaiHang.Mahang WHERE Tenhang = N'Son'";
            TimKiem1(query);
        }
    }
}
