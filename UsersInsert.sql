Create Table Users
(
Id int identity primary key,
Username nvarchar(100),
Password nvarchar(100)
)

Insert into Users values ('male','male')
insert into Users values ('female','female')