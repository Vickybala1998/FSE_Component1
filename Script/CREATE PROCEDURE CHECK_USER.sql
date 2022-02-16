CREATE PROCEDURE CHECK_USER

@user_id varchar(500),

@password varchar(200)

AS
BEGIN

if(@password=' ')
BEGIN
SELECT * FROM tbl_User where EMAIL = @user_id
END

else
BEGIN
SELECT * FROM tbl_User where EMAIL = @user_id and PASSWORD=@password
END
END