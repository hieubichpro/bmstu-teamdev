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

-- Chèn 10 dòng dữ liệu mẫu cho bảng users
INSERT INTO users (name, phone, address, email, login, password, role) VALUES
('John Doe', '1234567890', '123 Elm St', 'john@example.com', 'john_doe', 'password123', 'user'),
('Jane Smith', '0987654321', '456 Oak Ave', 'jane@example.com', 'jane_smith', 'pass456', 'admin'),
('Alice Johnson', '1112223333', '789 Pine Rd', 'alice@example.com', 'alice_j', 'alicepwd', 'user'),
('Bob Brown', '4445556666', '321 Maple Dr', 'bob@example.com', 'bobb', 'bobb123', 'user'),
('Charlie Davis', '7778889999', '654 Birch Blvd', 'charlie@example.com', 'charlied', 'charliepwd', 'user'),
('Diana Evans', '5556667777', '987 Cedar Ct', 'diana@example.com', 'dianae', 'dianapass', 'admin'),
('Evan Flores', '2223334444', '246 Walnut Way', 'evan@example.com', 'evanf', 'evanpass', 'user'),
('Fiona Garcia', '8889990000', '135 Hickory Ln', 'fiona@example.com', 'fionag', 'fiona123', 'user'),
('George Harris', '9990001111', '864 Spruce Cir', 'george@example.com', 'georgeh', 'georgepwd', 'user'),
('Hannah Irving', '6667778888', '753 Willow St', 'hannah@example.com', 'hannahi', 'hannahpass', 'user');
