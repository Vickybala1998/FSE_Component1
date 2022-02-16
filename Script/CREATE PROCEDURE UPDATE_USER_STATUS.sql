CREATE PROCEDURE UPDATE_USER_STATUS

(
@user_id varchar(500),

@is_active bit,

@logged_in dateTime,

@logged_out dateTIme
)
AS
BEGIN
if ( @is_active = 1)
BEGIN
INSERT INTO tbl_LoginStatus
(
USER_ID,

IS_ACTIVE,

LOGGED_IN
)

VALUES

(
(Select ID From tbl_User where EMAIL=@user_id),

@is_active,

@logged_in
)
END
else
BEGIN

UPDATE tbl_LoginStatus 
SET 
IS_ACTIVE = @is_active,

LOGGED_OUT = @logged_out

Where USER_ID = (Select ID From tbl_User where EMAIL=@user_id) and
IS_ACTIVE=1

END
END



