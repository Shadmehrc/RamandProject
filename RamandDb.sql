BEGIN TRANSACTION
BEGIN try
-- Create DataBase and tables

USE master
DROP Database IF EXISTS RamandDB
CREATE DATABASE RamandDB

USE RamandDB


CREATE TABLE [dbo].[User_Ramand](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[FullName]  [nvarchar](100) NOT NULL,
	[HashedPassword] [nvarchar](100) NOT NULL,
	[Age] [int] NOT NULL,
	[ProvinceTitle] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE Config
(
	ID							INT  identity(1,1)			,
	[Key]						VARCHAR(200)		 NOT NULL,
	Value						VARCHAR(200)		 NOT NULL,
)


CREATE TABLE Token
(
	ID						INT  identity(1,1)			,
	TokenHash				NVARCHAR(max)		 NOT NULL,
	RefreshTokenHash		NVARCHAR(max)	     NOT NULL,
	TokenExpire				DATE				 NOT NULL,
	RefreshTokenExpire		DATE				 NOT NULL,
	UserId					INT 				 NOT NULL,
	CreateDate				DATE				 NOT NULL,
	IsActive				BIT					 NOT NULL
)




-------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------

--Sp : Create User

Create PROCEDURE CreateUser
(
    @UserName AS VARCHAR(20),
    @HashedPassword AS NVARCHAR(100),
    @FullName AS NVARCHAR(100)  ,
    @ProvinceTitle AS NVARCHAR(50),
    @Age AS INT  
)
AS
BEGIN
    DECLARE @FoundedUser VARCHAR(20);

    SELECT @FoundedUser = UR.UserName
    FROM dbo.User_Ramand UR
	WHERE UR.UserName=@UserName;


    IF (@FoundedUser IS NULL)
    BEGIN
        BEGIN TRAN;
        BEGIN TRY
            INSERT INTO dbo.User_Ramand
            (
                UserName,
                FullName,
                HashedPassword,
                Age,
                ProvinceTitle,
				CreateDate
            )
            VALUES
            (   @UserName,       -- UserName - nvarchar(20)
                @FullName,       -- FullName - nvarchar(100)
                @HashedPassword, -- HashedPassword - nvarchar(100)
                @Age,            -- Age - int
                @ProvinceTitle ,  -- ProvinceTitle - nvarchar(50)
				GETDATE()
                );

            COMMIT;
            SELECT 1 AS IsSuccess,
                   'عملیات با موفقیت انجام شد.' AS ResultMessage;
            RETURN;
        END TRY
        BEGIN CATCH
            SELECT 0 AS IsSuccess,
                   'عملیات با خطا مواجه شد!' AS ResultMessage;
            ROLLBACK;
        END CATCH;
    END;
    ELSE
    BEGIN
        SELECT 0 AS IsSuccess,
               N'نام کاربری ' + @FoundedUser + N' از  قبل ثبت شده است.' AS ResultMessage;
    END;


END

-------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------

--Sp : GetUsersList


CREATE PROCEDURE GetUsersList
(
    @Id INT = NULL,
    @UserName VARCHAR(100) = NULL
)
AS
BEGIN
    SELECT *  FROM dbo.User_Ramand AS UR
    WHERE	  (@Id		 IS NULL    OR UR.ID = @Id )
          AND (@UserName IS NULL    OR UR.UserName = @UserName);
END;


-------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------

--SP: Validate User


CREATE PROCEDURE ValidateUser
(
	@UserName NVARCHAR(20)		 ,
	@HashedPassword NVARCHAR(50) 
)
AS
BEGIN	
    DECLARE @FoundedUser VARCHAR(20);

    SELECT @FoundedUser = UR.UserName
    FROM dbo.User_Ramand UR
	WHERE UR.UserName=@UserName;

	IF(@FoundedUser IS null)
	BEGIN
	    SELECT 0 AS IsSuccess,'نام کاربری یافت نشد!' AS ResultMessage, NULL AS UserId
		RETURN
	END
	ELSE
	BEGIN
    DECLARE @FoundedUserId int;

	    SELECT @FoundedUserId=ID FROM dbo.User_Ramand  
	WHERE (@UserName = UserName AND @HashedPassword = HashedPassword)	
	IF(@FoundedUserId IS NOT NULL)
	BEGIN
	    SELECT 1 AS IsSuccess, 'صحت اطلاعات تایید میگردد' AS ResultMessage,@FoundedUserId AS UserId
		RETURN
	END
	ELSE
    BEGIN
	    SELECT 0 AS IsSuccess, 'نام کاربری یا رمز عبور اشتباه است' AS ResultMessage, NULL AS UserId
        RETURN
    END

	END
	
END

-------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------
--Sp : Add Token 

CREATE PROCEDURE AddToken(
	
	@HashedToken				AS   NVARCHAR(max)		 ,
	@HashedRefreshToken		AS   NVARCHAR(max)	     ,
	@TokenExp			AS   DATE				 ,
	@RefreshTokenExp		AS   DATE				 ,
	@UserId					AS   INT 				 
)
AS
BEGIN
    INSERT INTO dbo.Token
    (
        TokenHash,
        RefreshTokenHash,
        TokenExpire,
        RefreshTokenExpire,
        UserId,
        CreateDate,
		IsActive
    )
    VALUES
    (   @HashedToken,       -- TokenHash - nvarchar(max)
        @HashedRefreshToken,       -- RefreshTokenHash - nvarchar(max)
        @TokenExp, -- TokenExpire - date
        @RefreshTokenExp, -- RefreshTokenExpire - date
        @UserId,         -- UserId - int
        GETDATE(),
		1-- CreateDate - date
        )

		SELECT 1 AS IsSuccess
END


-------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------

--Sp : Find Refresh Token


CREATE PROCEDURE FindRefreshToken
(
@RefreshToken NVARCHAR(MAX)
)
AS
BEGIN
    SELECT 
           T.TokenHash			 AS HashedToken,
           T.RefreshTokenHash    AS HashedRefreshToken,
           T.TokenExpire		 AS TokenExp,
           T.RefreshTokenExpire  AS RefreshTokenExp,
           T.UserId,
		   UR.UserName 
		   FROM dbo.Token T
		    JOIN dbo.User_Ramand UR
		   ON UR.ID=T.UserId
	WHERE T.RefreshTokenHash=@RefreshToken
		  AND T.IsActive=1
END



-------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------

-- Sp : Delete Token

CREATE PROCEDURE DeleteToken
(@UserId INT  )
AS
BEGIN
    UPDATE dbo.Token
    SET IsActive = 0
    WHERE UserId = @UserId;
	SELECT 1 AS IsSuccess
END;



-------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------
--Insert Configs:
INSERT INTO dbo.Config
(
    [Key],
    Value
)
VALUES
(   'issuer', -- Key - varchar(200)
    'Ramand'  -- Value - varchar(200)
    ),
(   'audience', -- Key - varchar(200)
    'Ramand'    -- Value - varchar(200)
),
(   'ExpiresInHour', -- Key - varchar(200)
    '10'       -- Value - varchar(200)
),
(   'RefreshTokenExpiresInHour', -- Key - varchar(200)
    '4320'),
(   'Key', -- Key - varchar(200)
    '{DC6A837B-4F30-4847-8839-A3E85B2A2DAC}');


-------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------
--Sp : Get Application Configs
CREATE PROCEDURE GetAppConfig
AS
BEGIN
    SELECT * FROM dbo.Config
END
-------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------


COMMIT TRANSACTION
END TRY
BEGIN CATCH
ROLLBACK
END catch
--Insert Data
--Password: 123456
--exec [CreateUser] @UserName=N'Ali',@HashedPassword=N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',@FullName=N'Alizade',@ProvinceTitle=N'Tehran',@Age=11
--exec [CreateUser] @UserName=N'Mohammad',@HashedPassword=N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',@FullName=N'Mohammadi',@ProvinceTitle=N'shiraz',@Age=12
--exec [CreateUser] @UserName=N'Taghi',@HashedPassword=N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',@FullName=N'Taghizade',@ProvinceTitle=N'isfahan',@Age=13

