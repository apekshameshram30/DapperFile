create database DapperDB
use DapperDB

Drop Table Companies;
SET IDENTITY_INSERT Companies ON;
Create Table Companies(
  CompanyId  int Primary Key Identity(1,1)not null,
  Name varchar(30),
  Address varchar(50),
  Country varchar(50)
);
Insert Into Companies(CompanyId,Name,Address,Country)
values (1, 'xyz', 'Nagpur','India'),
       (2,'pqrs', 'Mumbai','India'),
	   (3,'abcd','NewYork','UK'),
	   (4, 'mnop','wellington','new-zeland'),
	   (5,'wshtf','pune','India');

select * from Companies;


drop table Employees		
Create Table Employees(
   Id int Primary Key Identity(1,1)not null,
   Name varchar(30),
   Age int,
   Position varchar(40),
   CompanyId int,
   Foreign Key (CompanyId) References Companies(CompanyId)
);
Insert Into Employees(Name,Age,Position,CompanyId)
values('Apeksha',21,'DBA',1),
      ('Priya',23,'Mean',2),
	  ('Simran',21,'.NetDev',1),
	  ('Rushi',24,'Angular',4),
	  ('Anchal',21,'.Net',3);

select * from Companies;
Select * from Employees;
/*
Exec SpDapper_Sel  @Id = 1
select * from Companies Where CompanyId= @Id 

Select c.CompanyId, c.Name,c.Address,c.Country
from Companies c join Employees e on c.CompanyId=e.CompanyId
*/
Drop Procedure SpDapper_Sel;
Create Procedure SpDapper_Sel  
                              @Id int
as
begin
 
select * from Companies Where CompanyId= @Id

--      c.CompanyId, c.Name, c.Address, c.Country
--from Companies c join Employees  e on c.CompanyId=e.CompanyId
--where e.Id= @Id
end

-----SP for employee

--Exec SpEmployee_Sel  @Id =2
-- Select *From Companies Where Id=@Id

----
drop Procedure SpEmployee_Sel;
Create Procedure SpEmployee_Sel 
                                  @Id int

as
begin 

Select * from Employees where Id = @Id

end


--------------------------------------
delete from Employees Where id=6