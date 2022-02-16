CREATE PROCEDURE VIEW_TWEET

@user_id varchar(500)

AS
BEGIN

if(@user_id='ALL')
BEGIN
Select TWEET from tbl_Tweet
END

else
BEGIN
SELECT TWEET from tbl_tweet where USER_ID=(Select ID from tbl_User where EMAIL= @user_id)
END

END