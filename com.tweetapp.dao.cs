using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace com.tweetapp
{
    public class tweetappDao
    {
        public const string connectionString = "Data Source=LTIN235692\\SQLEXPRESS10;Initial Catalog=tweetApp_FSE;Integrated Security=True;Pooling=True;Min Pool Size=2;Max Pool Size = 5";

        #region properties
        private bool is_UserAlreadyRegister = false;
        private bool is_ResgistraionSuccess = false;
        private bool is_LoginSuccess = false;
        private bool is_PostTweetSuccess = false;

        public bool Is_UserAlreadyRegister { get; set; }
        public bool Is_ResgistraionSuccess { get; set; }
        public bool Is_LoginSuccess { get; set; }
        public bool Is_PostTweetSuccess { get; set; }
        #endregion

        /// <summary>
        /// Get All Registered Users
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllUsers()
        {
            List<string> users = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("GET_ALLUSERS", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                DbDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        users.Add(dataReader.GetString(0));
                    }
                }
                return users;
            }
        }

        /// <summary>
        /// Check User Availabilty
        /// </summary>
        /// <param name="user_Id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckUser(string user_Id, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("CHECK_USER", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@user_id", user_Id));
                sqlCommand.Parameters.Add(new SqlParameter("@password", password));
                DbDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    Is_UserAlreadyRegister = true;
                }
                return Is_UserAlreadyRegister;
            }
        }

        /// <summary>
        /// Register the new User
        /// </summary>
        /// <param name="first_Name"></param>
        /// <param name="last_Name"></param>
        /// <param name="gender"></param>
        /// <param name="dob"></param>
        /// <param name="email_Id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserRegistration(string first_Name, string last_Name, string gender, DateTime dob, string email_Id, string password)
        {
            int rowsAffected;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                if (!Is_UserAlreadyRegister)
                {
                    SqlCommand sqlCommand = new SqlCommand("USER_REGISTRAION", conn);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@first_name", first_Name));
                    sqlCommand.Parameters.Add(new SqlParameter("@last_name", last_Name));
                    sqlCommand.Parameters.Add(new SqlParameter("@gender", gender));
                    sqlCommand.Parameters.Add(new SqlParameter("@dob", dob));
                    sqlCommand.Parameters.Add(new SqlParameter("@email", email_Id));
                    sqlCommand.Parameters.Add(new SqlParameter("@password", password));

                    rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Is_ResgistraionSuccess = true;
                    }
                }
                return Is_ResgistraionSuccess;
            }
        }

        /// <summary>
        /// Post A new Tweet
        /// </summary>
        /// <param name="user_Id"></param>
        /// <param name="tweet"></param>
        public void PostTweet(string user_Id, string tweet)
        {
            int rowsAffected;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("POST_TWEET", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@user_id", user_Id));
                sqlCommand.Parameters.Add(new SqlParameter("@tweet", tweet));
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Is_PostTweetSuccess = true;
                }
                else
                {
                    Is_PostTweetSuccess = false;
                }

            }
        }

        /// <summary>
        /// view Specified User Tweets and All user Tweets
        /// </summary>
        /// <param name="user_Id"></param>
        /// <returns></returns>
        public List<string> ViewTweet(string user_Id)
        {
            List<string> tweets = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("VIEW_TWEET", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@user_id", user_Id));
                DbDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        tweets.Add(dataReader.GetString(0));
                    }
                }
            }
            return tweets;
        }

        /// <summary>
        /// Forgot and Reset Password
        /// </summary>
        /// <param name="user_Id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UpdatePassword(string user_Id, string password)
        {
            bool is_UpdatePasswordSuccess = false;
            int rowsAffected;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("UPDATE_PASSWORD", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@user_id", user_Id));
                sqlCommand.Parameters.Add(new SqlParameter("@password", password));

                rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    is_UpdatePasswordSuccess = true;
                }
            }
            return is_UpdatePasswordSuccess;
        }

        /// <summary>
        /// Maintain the User Login and Logout Status
        /// </summary>
        /// <param name="user_Id"></param>
        /// <param name="is_Active"></param>
        /// <param name="logged_In"></param>
        /// <param name="logged_Out"></param>

        public void UserStatus(string user_Id, bool is_Active, DateTime logged_In, DateTime logged_Out)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("UPDATE_USER_STATUS", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@user_id", user_Id));
                sqlCommand.Parameters.Add(new SqlParameter("@is_active", is_Active));
                sqlCommand.Parameters.Add(new SqlParameter("@logged_in", logged_In));
                sqlCommand.Parameters.Add(new SqlParameter("@logged_out", logged_Out));
                sqlCommand.ExecuteNonQuery();
            }
        }

    }
}
