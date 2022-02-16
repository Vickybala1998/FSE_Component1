
CREATE PROCEDURE USER_REGISTRAION
@first_name varchar(200),

@last_name varchar(200),

@gender varchar(50),

@dob date,

@email varchar(500),

@password varchar(200)

AS 
BEGIN 

INSERT INTO [dbo].[tbl_User]
(
 FIRST_NAME,

 LAST_NAME,

 GENDER,

 DOB,

 EMAIL,

 PASSWORD
)

VALUES
(
@first_name,

@last_name,

@gender,

@dob,

@email,

@password
)
END





