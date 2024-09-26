use master
Drop database WebBanGiay
CREATE DATABASE WebBanGiay
GO
USE WebBanGiay; -- Sử dụng cơ sở dữ liệu WebBanGiay
GO


-- Bảng Brand
CREATE TABLE Brands (
    BrandID INT PRIMARY KEY identity(1,1),
    BrandName NVARCHAR(50),
);
-- 
select * from Brands
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(255),
	ProductImage NVARCHAR(255),
    Description NVARCHAR(MAX),
    Price DECIMAL(10, 2),
    BrandID int
	foreign key(BrandID) references Brands(BrandID)
);
GO

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255),
    Password NVARCHAR(255),
    Email NVARCHAR(255),
    Address NVARCHAR(255),
    PhoneNumber VARCHAR(20),
    Role VARCHAR(50)
);
GO
-- Bảng Đơn hàng (Orders)
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY,
    UserID INT,
    OrderDate DATETIME,
    TotalAmount int,
    OrderStatus NVARCHAR(50) DEFAULT 'Xử lý',
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Bảng Chi tiết đơn hàng (OrderDetails)
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    Price DECIMAL(10, 2),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
GO

-- Thay đổi kiểu dữ liệu của cột TotalAmount thành int
ALTER TABLE Orders
ALTER COLUMN TotalAmount INT;

-- Thêm cột mới OrderStatusDefault và gán giá trị mặc định
ALTER TABLE Orders
ADD OrderStatusDefault NVARCHAR(50) DEFAULT 'Xử lý';

-- Cập nhật giá trị của OrderStatus từ cột mới
UPDATE Orders
SET OrderStatus = OrderStatusDefault;

-- Xóa ràng buộc mặc định
ALTER TABLE Orders
DROP CONSTRAINT DF__Orders__OrderSta__4316F928; -- Thay thế bằng tên ràng buộc của bạn

-- Xóa ràng buộc khóa ngoại (nếu có)
ALTER TABLE Orders
DROP CONSTRAINT FK_Orders_OrderStatusDefault; -- Thay thế bằng tên ràng buộc của bạn

-- Xóa cột OrderStatusDefault nếu không cần thiết
ALTER TABLE Orders
DROP COLUMN OrderStatusDefault;

-- Bảng Sản phẩm (Products)

-- Chèn dữ liệu vào bảng Brands
INSERT INTO Brands VALUES
('BrandA'),
('BrandB'),
('BrandC'),
('BrandD'),
('BrandE');

select * from Orders

-- Chèn dữ liệu vào bảng Products
INSERT INTO Products (ProductName, ProductImage, Description, Price, BrandID) VALUES
(N'Giày bóng rổ JorDan 1', 'Giay1.jpg', 'Description for ProductA', 20.5, 1),
(N'Giày Sneaker Lining', 'Giay2.jpg', 'Description for ProductB', 15.99, 2),
(N'Giày chạy bộ JorDan','Giay3.jpg', 'Description for ProductC', 30.75, 3),
(N'Giày sneaker JorDan 2', 'Giay4.jpg', 'Description for ProductD', 25.0, 4),
(N'Giày bóng rổ Lining', 'Giay5.jpg', 'Description for ProductF', 18.5, 5),
(N'Giày chạy bộ Lining', 'Giay6.jpg', 'Description for ProductG', 18.5, 5),
(N'Giày bóng rổ Peak 1', 'Giay7.jpg', 'Description for ProductH', 18.5, 2),
(N'Giày sneaker Peak Ex', 'Giay8.jpg', 'Description for ProductI', 18.5, 4),
(N'Giày sneaker Jordan Ex', 'Giay9.jpg', 'Description for ProductK', 18.5, 5),
(N'Giày thể thao Linin Pear', 'Giay10.jpg', 'Description for ProductZ', 18.5, 1)

-- Chèn dữ liệu vào bảng Users
INSERT INTO Users (Username, Password, Email, Address, PhoneNumber, Role) VALUES
('User1', 'Password1', 'user1@email.com', 'Address1', '123456789', 'Customer'),
('User2', 'Password2', 'user2@email.com', 'Address2', '987654321', 'Admin'),
('User3', 'Password3', 'user3@email.com', 'Address3', '456789012', 'Customer'),
('User4', 'Password4', 'user4@email.com', 'Address4', '321098765', 'Customer'),
('User5', 'Password5', 'user5@email.com', 'Address5', '876543210', 'Admin');
select * from Users

use master
go
create login DB_Admin_Backup
	with password = N'DB_Admin_Backup',
	check_expiration = off,
	check_policy = off,
	default_database = WebBanGiay
go

use WebBanGiay
go
create user DB_Admin_Backup
	for login DB_Admin_Backup
go

use WebBanGiay
go
	ALTER ROLE DB_Admin_Backup add member DB_Admin_Backup
go

use master
go
create login DB_User
	with password = N'DB_User',
	check_expiration = off,
	check_policy = off
go

use WebBanGiay
go
create user DB_User
	for login DB_User
go

use WebBanGiay
go
	grant select, insert, delete
	on dbo.Products to DB_User
go

use WebBanGiay
go
	revoke delete
	on dbo.Products from DB_User
go


