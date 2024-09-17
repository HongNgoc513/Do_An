using DoAnWeb.ThanhToan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace DoAn1.Pages
{
    public class GioHangModel : PageModel
    {
        public List<string> tenSanPhamList;
        public DataTable GioHang { get; set; }
        public void TimKiem1(string query)
        {
            using (SqlConnection con = new SqlConnection(SQLConnect.Conn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    GioHang = new DataTable();
                    adapter.Fill(GioHang);
                }
            }
        }
        public void GetTenSP(DataTable GioHang)
        {
            tenSanPhamList = new List<string>();

            string query = @"SELECT TenSanPham FROM SanPham WHERE MaSanPham = @maSP";

            using (SqlConnection con = new SqlConnection(SQLConnect.Conn))
            {
                con.Open();

                for (int i = 0; i < GioHang.Rows.Count; i++)
                {
                    string maSP = GioHang.Rows[i]["MaSanPham"].ToString();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@maSP", maSP);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string tenSP = reader["TenSanPham"].ToString();
                                tenSanPhamList.Add(tenSP);
                            }
                        }
                    }
                }
            }

        }
        public double GetGia()
        {
            double totalPrice = 0;
            string query = @"SELECT SUM(Gia) AS TotalPrice FROM GioHang";

            using (SqlConnection con = new SqlConnection(SQLConnect.Conn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Thực thi truy vấn và đọc kết quả
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalPrice = Convert.ToDouble(result);
                    }
                }
            }

            return totalPrice;
        }
        public string ID { get;set; }
        public void OnGet()
        {
            ID = "";
            ID = Request.Query["ID"];
            Console.WriteLine(ID);
            if (ID != "")
            {
                using (SqlConnection con = new SqlConnection(SQLConnect.Conn))
                {
                    con.Open();
                    string query1 = $@"DELETE FROM GioHang WHERE Masanpham = '{ID}'";
                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Deleted successfully"); // Thông báo xóa thành công
                        }
                        else
                        {
                            Console.WriteLine("No rows deleted"); // Thông báo không có hàng nào được xóa
                        }
                    }
                }
            }
            if (GioHang != null)
            {
                GioHang.Clear();
            }
            string query = $@"SELECT * FROM GioHang WHERE Tendangnhap = N'{User.Identity.Name}'";
            TimKiem1(query);
            GetTenSP(GioHang);
            double gia = GetGia();
            PaymentInformationModel.Amount = gia;
        }
    }
}
