USE master
GO

-- Check if the database 'MP' exists, and drop it if it does
IF EXISTS (SELECT * FROM sysdatabases WHERE name='MP')
BEGIN
    ALTER DATABASE MP SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE MP
END
GO

-- Create a new database named 'MP'
CREATE DATABASE MP
GO

USE MP
GO

-- Create the Users table
CREATE TABLE Users(
    Tendangnhap nvarchar(50),
    Matkhau nvarchar(50),
    Users nvarchar(50),
    Quyenhan nvarchar(50),
    DiaChi nvarchar(30),
    SoDT nvarchar(10),
    PRIMARY KEY (Tendangnhap)
);

-- Create the LoaiHang table
CREATE TABLE LoaiHang(
    Mahang nvarchar(10),
    Tenhang nvarchar(50),
    Soluongnhap int,
    PRIMARY KEY (Mahang)
);

-- Create the SanPham table
CREATE TABLE SanPham (
    Masanpham nvarchar(10),
    Mahang nvarchar(10),
    Tensanpham nvarchar(50),
    Gia int,
	img nvarchar(20),
    PRIMARY KEY (Masanpham),
    FOREIGN KEY (Mahang) REFERENCES LoaiHang(Mahang)
);

-- Create the HoaDon table
CREATE TABLE HoaDon (
    MaHD nvarchar(10),
    Ngaytao date,
    Tongtien int,
    Tendangnhap nvarchar(50),
    PRIMARY KEY (MaHD),
    FOREIGN KEY (Tendangnhap) REFERENCES Users(Tendangnhap)
);

-- Create the ChiTietHoaDon table
CREATE TABLE ChiTietHoaDon (
    MaHD nvarchar(10),
    Masanpham nvarchar(10),
    Ngaymua date,
	Tongtien int,
    PRIMARY KEY (MaHD, Ngaymua),
    FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
    FOREIGN KEY (Masanpham) REFERENCES SanPham(Masanpham)
);
-- Create the GioHang table
CREATE TABLE GioHang (
    Tendangnhap nvarchar(50),
    Masanpham nvarchar(10),
	TenSanPham nvarchar(20),
    Soluong int,
    Gia int,
    ThanhTien int,
    PRIMARY KEY (Tendangnhap, Masanpham), -- Sử dụng tên đăng nhập và mã sản phẩm làm khóa chính
    FOREIGN KEY (Tendangnhap) REFERENCES Users(Tendangnhap),
    FOREIGN KEY (Masanpham) REFERENCES SanPham(Masanpham)
);
SELECT * FROM GioHang
-- Set date format
SET DATEFORMAT dmy;
-- Add constraint to Users table for QuyenHan
ALTER TABLE Users
ADD CONSTRAINT CK_QuyenHan CHECK (QuyenHan IN ('Admin', 'KhachHang'));
-- Thêm dữ liệu vào bảng Users
INSERT INTO Users (Tendangnhap, Matkhau, Users, Quyenhan, DiaChi, SoDT) VALUES
('user1', 'password1', N'Người dùng 1', 'Admin', N'Địa chỉ 1', '123456789'),
('user2', 'password2', N'Người dùng 2', 'KhachHang', N'Địa chỉ 2', '987654321'),
('user3', 'password3', N'Người dùng 3', 'KhachHang', N'Địa chỉ 3', '456123789'),
('user4', 'password4', N'Người dùng 4', 'KhachHang', N'Địa chỉ 4', '321654987'),
('user5', 'password5', N'Người dùng 5', 'KhachHang', N'Địa chỉ 5', '789321654');

-- Thêm dữ liệu vào bảng LoaiHang
INSERT INTO LoaiHang (Mahang, Tenhang, Soluongnhap) VALUES
('MH001', N'Son', 100),
('MH002', N'Phan phu ', 200),
('MH003', N'Cushion/nen', 150),
('MH004', N'Tay trang', 150),
('MH005', N'Sua rua mat', 150),
('MH006', N'Tay te bao chet', 150),
('MH007', N'Duong the', 150);
SELECT * FROM LoaiHang
-- Thêm dữ liệu vào bảng SanPham
INSERT INTO SanPham (Masanpham, Mahang, Tensanpham, Gia,img) VALUES
('SP001', 'MH001', N'Son Dior', 200000,'baner'),
('SP002', 'MH001', N'Son Bbia', 200000,'bbia'),
('SP003', 'MH001', N'Son Black Rouge', 200000,'br'),
('SP004', 'MH001', N'Son dưỡng Cocoon', 200000,'duongmoi1'),
('SP005', 'MH001', N'Son maybeline siêu lì', 200000,'may'),
('SP006', 'MH001', N'Son Merzy Water', 200000,'merzy'),
('SP007', 'MH001', N'Son bóng Ofelia', 200000,'ofe'),
('SP008', 'MH001', N'Son bóng romand', 200000,'romand2'),
('SP009', 'MH001', N'Son Lauder', 200000,'sonLauder'),
('SP010', 'MH001', N'Son Zesea', 200000,'zesea'),
('SP011', 'MH001', N'Son thỏi black Rouge', 200000,'br1'),
('SP012', 'MH001', N'Son espoir', 200000,'es'),
('SP013', 'MH002', N'Phấn phủ Chanel', 250000,'phanchanel'),
('SP014', 'MH002', N'Phấn phủ Chanel bột', 250000,'phanchanel1'),
('SP015', 'MH002', N'Phấn phủ Dior', 250000,'phandior'),
('SP016', 'MH002', N'Phấn phủ Chanel ver2', 250000,'phandior1'),
('SP017', 'MH002', N'Phấn phủ Maybeline', 250000,'phanmay'),
('SP018', 'MH003', N'Nền Mac', 250000,'nenmac'),
('SP019', 'MH004', N'Tẩy trang bí đao Cocoon', 150000,'taytrang'),
('SP020', 'MH004', N'Dầu tẩy trang hoa hồng Cocoon', 150000,'taytrang2'),
('SP021', 'MH004', N'Tẩy trang Garnier Vitamin C', 150000,'ttgar'),
('SP022', 'MH004', N'Tẩy trang vichy', 150000,'ttvichy'),
('SP023', 'MH004', N'Nước tẩy trang Laroche-posay', 150000,'ttla'),
('SP024', 'MH005', N'Sữa rửa mặt Vichy', 300000,'srmvichy'),
('SP025', 'MH005', N'Sữa rửa mặt trà xanh Centella', 300000,'srmcen'),
('SP026', 'MH005', N'Sữa rửa mặt Centella', 300000,'srmcen1'),
('SP027', 'MH006', N'Tẩy tế bào chết body Cocoon', 50000,'ttbc'),
('SP028', 'MH007', N'Sữa dưỡng thể Vaseline', 50000,'va');

-- Thêm dữ liệu vào bảng HoaDon
INSERT INTO HoaDon (MaHD, Ngaytao, Tongtien, Tendangnhap) VALUES
('HD001', '2024-04-16', 500000, 'user1'),
('HD002', '2024-04-17', 800000, 'user2'),
('HD003', '2024-04-18', 150000, 'user3'),
('HD004', '2024-04-18', 120000, 'user4'),
('HD005', '2024-04-19', 300000, 'user5');

-- Thêm dữ liệu vào bảng ChiTiet

SELECT SUM(Gia)
FROM GioHang


SELECT * 
FROM LoaiHang 

SELECT * 
FROM SanPham
SELECT * FROM LoaiHang INNER JOIN SanPham  ON LoaiHang.Mahang = SanPham.Mahang


