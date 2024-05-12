CREATE TABLE Staff(
	f_name varchar(30),
	l_name varchar(30),
	staff_id int  PRIMARY KEY IDENTITY	,
	email varchar(50),
	adress varchar(100),
	username varchar(30) UNIQUE,
	password varchar(20), 
	s_birthday date,
	s_phone varchar(20),
)
ALTER TABLE Staff
ALTER COLUMN adress VARCHAR(100);


INSERT INTO Staff(f_name,l_name,email,adress,username,password,s_birthday,s_phone) VALUES ('Yasir','Sayar','yasirrsayarr@hotmail.com','aybey mah muzaffer mert sok no:27 uþak','yasir645',1234,'2003-12-11','05431838985')
INSERT INTO Staff(f_name,l_name,email,adress,username,password,s_birthday,s_phone) VALUES ('Çaðrý','Ergün','cagrihergun@hotmail.com','Ataþehir metropol bina 12 kat:21 , no:5','turkcelceo',12345,'1991','05412365464')

CREATE TABLE Employee(
	employee_id int PRIMARY KEY IDENTITY,
	employee_name varchar(30),
	employee_lastname varchar(30),
	employee_adress varchar(60),
	emp_username varchar(30) UNIQUE,
	emp_password varchar(20),
	emp_birthday date,
	emp_phone varchar(20),
)
ALTER TABLE Employee
ADD product_id int FOREIGN KEY REFERENCES Product(product_id) ;

INSERT INTO Employee (employee_name,employee_lastname,employee_adress,emp_username,emp_password,emp_birthday,emp_phone) VALUES ('Oðuz','Alpdoðan','Ahmet Yesevi Mahallesi 1 Dekar Sk. no:12 Pendik','oguzalpp','456789','1990-5-16','5356546516')
INSERT INTO Employee (employee_name,employee_lastname,employee_adress,emp_username,emp_password,emp_birthday,emp_phone) VALUES ('Serkan','Can','Küçük Çamlýca Mahallesi 27 Çiçek Sokaðý  Üsküdar','ninjaseko','ninjaseko64','1993-6-12','5465165158')

--SELECT * FROM Staff FULL JOIN Employee ON Staff.staff_id=employee_id

SELECT username,password FROM Staff
WHERE username='ninjaseko' AND password='ninjaseko64'
UNION ALL
SELECT emp_username,emp_password FROM Employee --UserName ile Þifreleri çýkarma iþlemi 
WHERE emp_username='ninjaseko' AND emp_password='ninjaseko64'



CREATE TABLE Customer(
	Customer_id int PRIMARY KEY IDENTITY,
	cf_name varchar(30),
	cl_name varchar(30),
	c_adress varchar(100),
	stf_id int FOREIGN KEY REFERENCES Staff(staff_id) on update cascade,
)
ALTER TABLE Customer
ALTER COLUMN c_adress VARCHAR(100);

INSERT INTO Customer (cf_name,cl_name,c_adress,stf_id) VALUES ('Ahmet','Can','Kaput mahallesi, Atatürk caddesi, 89. Sokak, 6 / 3 Þiþli , Ýstanbul',1)
INSERT INTO Customer (cf_name,cl_name,c_adress,stf_id) VALUES ('Ýbrahim','Ulusoy','maden mahallesi, Atatürk caddesi, 72. Sokak, 6 / 12 Þiþli , Ýstanbul',2)
SELECT Staff.f_name,Staff.l_name,Customer_id,cf_name,cl_name,c_adress FROM Customer INNER JOIN Staff ON Staff.staff_id=Customer.stf_id where Staff.staff_id=2;
CREATE TABLE Orders(
	Order_id int PRIMARY KEY IDENTITY,
	status int check(status in (1,0,-1)),
	cst_id int FOREIGN KEY REFERENCES Customer(Customer_id) on update cascade,
)

--ORDERLÝÝÝÝÝÝÝÝÝÝÝÝNNNNNNNNNNNNNEEEEEEEEEEEEEEEEEEEEEEEE

CREATE TABLE Product(
	product_id int PRIMARY KEY IDENTITY(200,1),
	p_title varchar(40),
	quantity int,
)
ALTER TABLE Product
ADD part_id int FOREIGN KEY REFERENCES Part(part_id) ON UPDATE CASCADE;

ALTER TABLE Product
ALTER COLUMN p_title varchar(40);
CREATE TABLE OrderLine (
    ---OrderLine_id int PRIMARY KEY IDENTITY,
    Order_id int,
    product_id int,
    orderQuantity int,
    FOREIGN KEY (Order_id) REFERENCES Orders(Order_id) ON UPDATE CASCADE,
    FOREIGN KEY (product_id) REFERENCES Product(product_id) ON UPDATE CASCADE
);
ALTER TABLE OrderLine
DROP COLUMN OrderLine_id;

Select * FROM OrderLine INNER JOIN Orders ON OrderLine.Order_id=Orders.Order_id 

CREATE TABLE Assembling (
    assembling_id int PRIMARY KEY IDENTITY,
    employee_id int,
    product_id int,
    FOREIGN KEY (employee_id) REFERENCES Employee(employee_id) ON UPDATE CASCADE,
    FOREIGN KEY (product_id) REFERENCES Product(product_id) ON UPDATE CASCADE
);

UPDATE Staff set password='ýmýt4799' where staff_id=6

SELECT f_name as Ad, l_name as Soyad, staff_id as id, email, adress, s_birthday as DogumTarihi, s_phone as TelefonNo
FROM Staff
WHERE
    f_name LIKE '' OR
    l_name LIKE 'ergün' OR
    email LIKE '' OR
    adress LIKE '' OR
    s_phone LIKE ''

	SELECT *
FROM Customer
JOIN Orders ON Customer.Customer_id = Orders.cst_id
JOIN OrderLine ON Orders.Order_id = OrderLine.Order_id
JOIN Product ON OrderLine.product_id = Product.product_id;

SELECT *
FROM Customer
JOIN Orders ON Customer.Customer_id = Orders.cst_id
JOIN OrderLine ON Orders.Order_id = OrderLine.Order_id
JOIN Product ON OrderLine.product_id = Product.product_id
WHERE Orders.Order_id = 2
  AND Customer.Customer_id = 3;
  ---adres sil --stf id sil --  cst_id sil ---
  --order id dursun 

  ---Customer_id,cf_name,cl_name,Order_ID,Status,Order_id,orderQuantity,p_title görünecek
SELECT Customer.Customer_id, Customer.cf_name, Customer.cl_name, Orders.Order_id, Orders.status, OrderLine.orderQuantity, Product.p_title, Customer.stf_id,Customer.Customer_id
FROM Customer
JOIN Orders ON Customer.Customer_id = Orders.cst_id
JOIN OrderLine ON Orders.Order_id = OrderLine.Order_id
JOIN Product ON OrderLine.product_id = Product.product_id
where stf_id=2
SELECT Order_id FROM Orders




CREATE TABLE Part(
	part_id int PRIMARY KEY IDENTITY(200,1),
	part_name varchar(40),
	part_quantity int,
)

SELECT *
FROM Product
INNER JOIN Part ON Product.part_id = Part.part_id;

CREATE TABLE Suplier(
	suplier_id int PRIMARY KEY IDENTITY(200,1),
	sup_name varchar(40),
	adress varchar(100),
)
ALTER TABLE Part
ADD suplier_id int FOREIGN KEY REFERENCES Suplier(suplier_id) ON UPDATE CASCADE;


SELECT Product.p_title as Ürün,Product.quantity as Adet ,Part.part_name as [Parça Adý],Product.product_id , Employee.employee_name,Employee.employee_lastname, Product.part_id FROM Product INNER JOIN Part ON Product.part_id = Part.part_id INNER JOIN Employee ON Employee.product_id=Product.product_id
SELECT 
    Product.p_title as Ürün,
    Product.quantity as Adet,
    Part.part_name as [Parça Adý],
    Product.product_id,
    Employee.employee_name,
    Employee.employee_lastname,
    Product.part_id 
FROM 
    Product 
INNER JOIN 
    Part ON Product.part_id = Part.part_id 
LEFT JOIN 
    Employee ON Employee.product_id = Product.product_id;

	SELECT Part.part_name as [Parça Adý], Part.part_quantity as Adet , Suplier.sup_name as [Tedarikçi],Part.part_id FROM Part INNER JOIN Suplier ON Part.suplier_id=Suplier.suplier_id

	SELECT employee_name as Ad,employee_lastname as Soyad,employee_adress,emp_birthday as DogumTarihi,emp_phone as TelefonNo FROM Employee INNER JOIN Product ON Employee.product_id=Product.product_id
