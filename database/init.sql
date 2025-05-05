create table Users(
	Id serial primary key,
	Name varchar(50) not null,
	Login varchar(50) not null,
	Password varchar(50) not null,
	Address varchar(50) not null,
	Phone varchar(50) not null,
	Email varchar(50) not null,
	Role varchar(50) not null
);

create table products (
	Id serial primary key,
	Name varchar(50) not null,
	Price int not null,
	Quantity int not null,
	Description varchar(50) not null,
	Image varchar(50) not null
);

create table carts(
	Id serial primary key,
	data_created date not null,
	id_user int references Users(Id) ON DELETE CASCADE
);

create table itemcarts(
	Id serial primary key,
	id_product int not null,
	id_cart int not null,
	quantity int not null,
	foreign key (id_cart) references Carts(Id) ON DELETE CASCADE,
	foreign key (id_product) references Products(Id) ON DELETE CASCADE
);

create table orders(
	Id serial primary key,
	status varchar(20) not null,
	data_created date not null,
	id_user int not null,
	foreign key (id_user) references users(Id) ON DELETE CASCADE
);

create table itemorders(
	Id serial primary key,
	id_product int not null,
	id_order int not null,
	quantity int not null,
	foreign key (id_order) references Orders(Id) ON DELETE CASCADE,
	foreign key (id_product) references Products(Id) ON DELETE CASCADE
);

COPY users(name, login, password, address, phone, email, role)
FROM '/docker-entrypoint-initdb.d/UserTable.csv'
DELIMITER ','
CSV HEADER;

COPY products(name, price, quantity, description, image)
FROM '/docker-entrypoint-initdb.d/ProductTable.csv'
DELIMITER ','
CSV HEADER;

COPY carts(data_created, id_user)
FROM '/docker-entrypoint-initdb.d/CartTable.csv'
DELIMITER ','
CSV HEADER;

COPY orders(status, data_created, id_user)
FROM '/docker-entrypoint-initdb.d/OrderTable.csv'
DELIMITER ','
CSV HEADER;

COPY itemcarts(id_product, id_cart, quantity)
FROM '/docker-entrypoint-initdb.d/ItemCartTable.csv'
DELIMITER ','
CSV HEADER;

COPY itemorders(id_product, id_order, quantity)
FROM '/docker-entrypoint-initdb.d/ItemOrderTable.csv'
DELIMITER ','
CSV HEADER;