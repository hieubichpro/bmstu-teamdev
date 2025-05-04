-- Tạo bảng users
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    phone VARCHAR(100),
    address VARCHAR(100),
    email VARCHAR(100),
    login VARCHAR(100),
    password VARCHAR(100),
    role VARCHAR(100)
);

-- Tạo bảng products
CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    price NUMERIC,
    quantity INT,
    description VARCHAR(100)
);

-- Tạo bảng carts
CREATE TABLE carts (
    id SERIAL PRIMARY KEY,
    data_created TIMESTAMP,
    id_user INT REFERENCES users(id)
);

-- Tạo bảng itemcarts
CREATE TABLE itemcarts (
    id SERIAL PRIMARY KEY,
    id_product INT REFERENCES products(id),
    id_cart INT REFERENCES carts(id),
    quantity INT
);

-- Tạo bảng orders
CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    status VARCHAR(255),
    data_created TIMESTAMP,
    id_user INT REFERENCES users(id)
);

-- Tạo bảng itemorders
CREATE TABLE itemorders (
    id SERIAL PRIMARY KEY,
    id_product INT REFERENCES products(id),
    id_order INT REFERENCES orders(id),
    quantity INT
);

-- Chèn dữ liệu vào bảng users
INSERT INTO users (name, phone, address, email, login, password, role) VALUES
('John Doe', '1234567890', '123 Elm St', 'john@example.com', 'john_doe', 'password123', 'User'),
('Jane Smith', '0987654321', '456 Oak Ave', 'jane@example.com', 'jane_smith', 'pass456', 'Admin'),
('Alice Johnson', '1112223333', '789 Pine Rd', 'alice@example.com', 'alice_j', 'alicepwd', 'User'),
('Bob Brown', '4445556666', '321 Maple Dr', 'bob@example.com', 'bobb', 'bobb123', 'User'),
('Charlie Davis', '7778889999', '654 Birch Blvd', 'charlie@example.com', 'charlied', 'charliepwd', 'User'),
('Diana Evans', '5556667777', '987 Cedar Ct', 'diana@example.com', 'dianae', 'dianapass', 'Admin'),
('Evan Flores', '2223334444', '246 Walnut Way', 'evan@example.com', 'evanf', 'evanpass', 'User'),
('Fiona Garcia', '8889990000', '135 Hickory Ln', 'fiona@example.com', 'fionag', 'fiona123', 'User'),
('George Harris', '9990001111', '864 Spruce Cir', 'george@example.com', 'georgeh', 'georgepwd', 'User'),
('Hannah Irving', '6667778888', '753 Willow St', 'hannah@example.com', 'hannahi', 'hannahpass', 'User');

-- Chèn dữ liệu vào bảng products
INSERT INTO products (name, price, quantity, description) VALUES
('Laptop ASUS X515', 750.00, 10, '15.6 inch, Intel i5, 8GB RAM'),
('iPhone 13', 999.99, 15, 'Apple smartphone with A15 chip'),
('Samsung Galaxy S22', 899.99, 20, 'Latest Samsung flagship'),
('Headphones Sony WH-1000XM4', 349.99, 25, 'Noise-canceling over-ear headphones'),
('Keyboard Logitech K380', 39.99, 50, 'Wireless multi-device keyboard'),
('Monitor LG 27UL500', 279.99, 12, '27 inch 4K UHD display'),
('USB-C Hub', 25.99, 40, '6-in-1 USB-C hub with HDMI'),
('External HDD 1TB', 59.99, 30, 'Portable USB 3.0 hard drive'),
('Mouse Logitech M185', 14.99, 60, 'Wireless optical mouse'),
('Smartwatch Amazfit Bip U', 69.99, 18, 'Fitness tracker with SpO2 sensor');

-- Chèn dữ liệu vào bảng carts
INSERT INTO carts (data_created, id_user) VALUES
(NOW(), 1),
(NOW(), 2),
(NOW(), 3),
(NOW(), 4),
(NOW(), 5);

-- Chèn dữ liệu vào bảng itemcarts
INSERT INTO itemcarts (id_product, id_cart, quantity) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 2, 1),
(5, 2, 1),
(4, 3, 2),
(6, 4, 1),
(8, 5, 3),
(10, 5, 1);

-- Chèn dữ liệu vào bảng orders
INSERT INTO orders (status, data_created, id_user) VALUES
('Pending', NOW(), 1),
('Shipped', NOW(), 2),
('Delivered', NOW(), 3),
('Cancelled', NOW(), 4),
('Pending', NOW(), 5);

-- Chèn dữ liệu vào bảng itemorders
INSERT INTO itemorders (id_product, id_order, quantity) VALUES
(1, 1, 1),
(2, 1, 1),
(3, 2, 1),
(4, 2, 2),
(5, 3, 1),
(6, 3, 1),
(7, 4, 2),
(8, 5, 1);
