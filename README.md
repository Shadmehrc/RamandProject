| Attribute	 | Details |
| ------------- | ----------- |
| ORM  | Dapper  |
| DataBase  | SQL Server  |
| Architecture  | Clean  |
|Auth system |JWT|
|Test |XUnit|
|MicroService |& DTO Based|
<br/>
<br/>
This repository contains a Microservice application that has Two services. One for authenticating users and giving JWT token and the other one to give all user data if the User's request was authorized.
<br/>
<br/>

Here Are All SQL Commands Of AcsessUser Database(AuthDB):
<br/>
```
USE master
DROP Database IF EXISTS AuthDB
CREATE DATABASE AuthDB
GO

USE AuthDB
GO

CREATE TABLE UsersAccessInfo
(
	PersonID INT primary key identity,
	UserName NVARCHAR(30) NOT NULL,
	HashedPassword NVARCHAR(50) NOT NULL,
)
GO

USE AuthDB
INSERT INTO UsersAccessInfo
(UserName,HashedPassword)
VALUES
('Alireza','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI='),
('Abbas','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI='),
('Nazanin','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI='),
('Hamid','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI='),
('Arash','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI='),
('Sohrab','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI='),
('Arezo','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI='),
('Keyhan','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=')
GO


CREATE PROCEDURE USP_FindUser
(
	@UserName NVARCHAR(30),
	@HashedPassword NVARCHAR(50)
)
AS
BEGIN	
	SELECT *
	FROM UsersAccessInfo  
	WHERE (@UserName = UserName AND @HashedPassword = HashedPassword)	
END
GO

EXEC USP_FindUser @UserName='Abbas',@HashedPassword='jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI='
Go

```
<br/>
<br/><br/>
<br/>
And There is All SQL Commands Of FullUsersInfo Database(UserInfoDB):<br/><br/>

> **_NOTE:_**  (the original string of all passwords is "123456")
 <br/>

```
USE master
DROP Database IF EXISTS UserInfoDB
CREATE DATABASE UserInfoDB
GO

USE UserInfoDB
GO

CREATE TABLE UsersFullInfos
(
	ID INT primary key identity,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Country NVARCHAR(30) NOT NULL,
	City NVARCHAR(30) NOT NULL,
	BirthDate CHAR(10) NOT NULL,
	NationalCode BIGINT NOT NULL,
	IsMarried bit NOT NULL,
	UserAccessID INT UNIQUE NOT NULL
)
GO

USE UserInfoDB
INSERT INTO UsersFullInfos
(FirstName,LastName ,Country ,City ,BirthDate ,NationalCode ,IsMarried ,UserAccessID)
VALUES
('Alireza','Aleagha' ,'Iran' ,'Tehran' ,'2000-01-04',0011597884 ,0,1),
('Abbas','Joza' ,'Iran' ,'Shiraz' ,'1999-02-24',0011526884 ,1,2),
('Nazanin','Mahdavi' ,'Iran' ,'Tehran' ,'2002-11-02',0011597884 ,0,3),
('Hamid','Ebrahimi' ,'Iran' ,'Rasht' ,'2001-03-08',0011597884 ,0,4),
('Arash','Ramezani' ,'Iran' ,'Araq' ,'1997-04-15',0011747884 ,1,5),
('Sohrab','Tavakoli' ,'Iran' ,'esfahan' ,'2000-12-10',0011595884 ,0,6),
('Arezo','Tadbiri' ,'Iran' ,'hamedan' ,'2005-11-25',0061597888 ,0,7),
('Keyhan','Damavandi' ,'Iran' ,'Tehran' ,'1998-07-16',0018557889 ,1,8)
GO

CREATE PROCEDURE USP_GetAllUsers
AS
BEGIN	
	SELECT *
	FROM UsersFullInfos
END
GO

EXEC USP_GetAllUsers 
Go

```
