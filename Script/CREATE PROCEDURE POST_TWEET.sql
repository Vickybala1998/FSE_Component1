CREATE PROCEDURE POST_TWEET

@user_id varchar(500),

@tweet varchar(500)

AS

BEGIN

INSERT INTO tbl_Tweet
(
  TWEET,

  USER_ID
)

VALUES
(
  @tweet,

  (Select ID from tbl_User Where EMAIL=@user_id)

)

END