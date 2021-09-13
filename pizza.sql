create database dbPizzaProject
use dbPizzaProject
create table Users 
(
email varchar (50) primary key,
name varchar (50),
password varchar (50),
adress varchar (50),
phone varchar (50)
)

insert into Users values ('Oleksandr@gmail.com','Oleksandr', '123', '12, Plain  street', '+93434328')
insert into Users values ('Ajhit@gmail.com','Ajhit', '324', '2, Plain  street', '+9343434328')



select * from Users
create table pizzaName 
(
pizza_id  int identity (1,1) primary key,
name varchar (50),
price float,
type varchar (50),
)
insert into pizzaName values ('Margeritta', 15, 'Plain')
insert into pizzaName values ('Mexicano', 25, 'Spicy')
insert into pizzaName values ('Cheeses', 15, 'Cheezy')


create table Toppings 
(
topping_id int identity (1,1) primary key,
name varchar (50),
Price float,
)

insert into Toppings values ('Olives', 2)
insert into Toppings values ('Tomato', 5)
insert into Toppings values ('Onion', 4)

select * from Toppings



create table Orders
(
order_id int identity (1,1) primary key,
user_id varchar (50) foreign key references Users (email),
status varchar (50), 
totalPrice float,
delivercharge varchar (50),
)

create table OrdersDetails
(
ordersDetails_id int identity (1,1) primary key,
order_id int foreign key references Orders (order_id),
pizza_number int foreign key references pizzaName (pizza_id), 
)

create table OrdersNumberDetails
(
ordersNumberDetails_id int foreign key references OrdersDetails (ordersDetails_id),
toppping_id int foreign key references Toppings (topping_id),
)

