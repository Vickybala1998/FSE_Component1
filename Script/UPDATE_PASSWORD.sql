CREATE PROCEDURE UPDATE_PASSWORD

@user_id varchar(500),

@password varchar(200)

AS
BEGIN
UPDATE tbl_User
SET
PASSWORD=@password where EMAIL=@user_id
END














