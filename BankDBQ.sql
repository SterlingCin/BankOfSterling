create table userLogin(
phoneNum int primary key,
userName varChar(15) unique,
userPass varChar(25),
firstName varChar(30),
lastName varchar(30),
email varChar(50)
)

create table adminLogin(
empNum int identity,
adminName varChar(20) unique,
adminPass varChar(20)
)

create table accounts(
accountNum int identity primary key,
phoneNum int foreign key references userLogin (phoneNum),
accBal decimal,
accType varChar(10),
)

create table transactions(
OriginAccNum int foreign key references accounts(accountNum),
OriginPrevBal decimal,
transIDNum int identity,
AmountSent decimal,
TransAccNum int,
dateOfTrans Date default (getdate())
)
Alter table transactions 
drop column OriginPrevBal 


insert into userLogin values(8675309, 'Cerritos', 'LowerDecks' ,'Brad', 'Boimler', 'DiscoveryKS@gmail.com')
insert into adminLogin values('BOfST','SilverBank')

insert into accounts values(8675309,600.00,'checking')
insert into accounts values(8675309,10000.00,'saving')

select * from userLogin
select * from accounts
select * from transactions
select * from adminLogin

update accounts set accBal = accBal - 200.00 where accountNum = 2
update accounts set accBal = accBal + 200.00 where accountNum = 1
insert into transactions values(2,200.00,1,GETDATE())