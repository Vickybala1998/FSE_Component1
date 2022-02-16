using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace com.tweetapp
{
    public class tweetappService
    {
        /// <summary>
        /// Home Page Menu
        /// </summary>
        public static void HomePage()
        {
            int flag = 0;

            while (flag == 0)
            {
                Console.WriteLine("Menu \n");
                Console.WriteLine("1.REGISTER \n");
                Console.WriteLine("2.LOG IN \n");
                Console.WriteLine("3.FORGET PASSWORD\n----------------------------------------------\n");

                int user_Input = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n");

                switch (user_Input)
                {
                    case 1:
                        flag = 1;
                        UserRegisteration();
                        break;

                    case 2:
                        flag = 1;
                        UserLogin();
                        break;

                    case 3:
                        flag = 1;
                        bool is_passwordUpdate = false;
                        while (!is_passwordUpdate)
                        {
                            Console.WriteLine("Enter User Id");
                            string user_id = Console.ReadLine();
                            is_passwordUpdate = UpdatePassword(user_id, string.Empty);
                        }
                        break;

                    default:
                        flag = 0;
                        Console.WriteLine("Please give correct input");
                        break;
                }
            }
        }

        /// <summary>
        /// New User Registration
        /// </summary>
        public static void UserRegisteration()
        {
            string first_Name;
            string last_Name;
            string gender;
            DateTime dob;
            string email_Id;
            string password;
            string confrim_Password;

            bool is_UserAvailable;
            bool is_Registraion = false;

            while (!is_Registraion)
            {
                try
                {
                    Console.WriteLine("Enter First Name*");
                    first_Name = Console.ReadLine();
                    Console.WriteLine("Enter Last Name");
                    last_Name = Console.ReadLine();
                    Console.WriteLine("Enter Gender*");
                    gender = Console.ReadLine();
                    Console.WriteLine("Enter DOB*");
                    DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob);
                    Console.WriteLine("Enter Email*");
                    email_Id = Console.ReadLine();
                    Console.WriteLine("Enter Password*");
                    password = Console.ReadLine();
                    Console.WriteLine("Enter Confrim Password*");
                    confrim_Password = Console.ReadLine();
                    Console.WriteLine("\n");


                    if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confrim_Password) &&
                        confrim_Password.Equals(password))
                    {
                        if (!String.IsNullOrEmpty(first_Name) && !string.IsNullOrEmpty(gender)
                             && !string.IsNullOrEmpty(email_Id))
                        {
                            tweetappDao data = new tweetappDao();
                            is_UserAvailable = data.CheckUser(email_Id, string.Empty);

                            if (is_UserAvailable)
                            {
                                Console.WriteLine("User Already Exists\n----------------------------------------------\n");
                            }
                            else
                            {
                                is_Registraion = data.UserRegistration(first_Name, last_Name, gender, dob, email_Id, password);
                                if (is_Registraion)
                                {
                                    is_Registraion = true;
                                    Console.WriteLine("Registration is Successfull\n----------------------------------------------\n");
                                    UserLogin();
                                }
                                else
                                {
                                    Console.WriteLine("Something Went Wrong\n----------------------------------------------\n");
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Filed * should not be empty\n----------------------------------------------\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Password is Mismatch Or Empty\n----------------------------------------------\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n----------------------------------------------\n");
                }
            }
        }

        /// <summary>
        /// User Login
        /// </summary>
        public static void UserLogin()
        {
            string user_Id;
            string password;
            bool is_LoginSuccessfull = false;
            while (!is_LoginSuccessfull)
            {
                try
                {
                    Console.WriteLine("Enter User Id");
                    user_Id = Console.ReadLine();
                    Console.WriteLine("Enter Password");
                    password = Console.ReadLine();
                    Console.WriteLine("\n");
                    tweetappDao data = new tweetappDao();
                    is_LoginSuccessfull = data.CheckUser(user_Id, password);
                    if (is_LoginSuccessfull)
                    {
                        data.UserStatus(user_Id, true, DateTime.Now, DateTime.Now);
                        Console.WriteLine("Welcome " + user_Id + "\n----------------------------------------------\n");
                        TweetPage(user_Id);
                    }
                    else
                    {
                        Console.WriteLine("Login Details Are Not Available\n----------------------------------------------\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n----------------------------------------------\n");
                }
            }
        }

        /// <summary>
        /// Loggedin User Page
        /// </summary>
        /// <param name="user_Id"></param>
        public static void TweetPage(string user_Id)
        {
            int flag = 0;
            while (flag == 0)
            {
                try
                {
                    Console.WriteLine("1.Post a Tweet");
                    Console.WriteLine("2.View My Tweets");
                    Console.WriteLine("3.View All Tweets");
                    Console.WriteLine("4.View All User");
                    Console.WriteLine("5.Rest Password");
                    Console.WriteLine("6.Log Out");
                    Console.WriteLine("\n----------------------------------------------\n");

                    int input = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n");
                    switch (input)
                    {
                        case 1:
                            tweetappDao data1 = new tweetappDao();
                            Console.WriteLine("Enter Your Tweet\n");
                            string tweet = Console.ReadLine();
                            data1.PostTweet(user_Id, tweet);
                            if (data1.Is_PostTweetSuccess)
                            {
                                Console.WriteLine("Your Tweet Was Sucessfully Posted\n----------------------------------------------\n");
                            }
                            else
                            {
                                Console.WriteLine("Failed to post your Tweet\n----------------------------------------------\n");
                            }
                            break;

                        case 2:
                            List<string> user_Tweets = new List<string>();
                            tweetappDao data2 = new tweetappDao();
                            user_Tweets = data2.ViewTweet(user_Id);
                            if (user_Tweets != null && user_Tweets.Count > 0)
                            {
                                foreach (string tweets in user_Tweets)
                                {
                                    Console.WriteLine(tweets + "\n");
                                }
                                Console.WriteLine("\n----------------------------------------------\n");
                            }
                            else
                            {
                                Console.WriteLine("No Tweets are available for this User");
                            }
                            break;

                        case 3:
                            user_Tweets = new List<string>();
                            tweetappDao data3 = new tweetappDao();
                            user_Tweets = data3.ViewTweet("ALL");
                            if (user_Tweets != null && user_Tweets.Count > 0)
                            {
                                foreach (string tweets in user_Tweets)
                                {
                                    Console.WriteLine(tweets + "\n");
                                }
                                Console.WriteLine("\n----------------------------------------------\n");
                            }
                            else
                            {
                                Console.WriteLine("No Tweets are available");
                            }
                            break;

                        case 4:
                            List<string> user_list = new List<string>();
                            tweetappDao data4 = new tweetappDao();
                            user_list = data4.GetAllUsers();
                            if (user_list != null && user_list.Count > 0)
                            {
                                foreach (string users in user_list)
                                {
                                    Console.WriteLine(users + "\n");
                                }
                                Console.WriteLine("\n----------------------------------------------\n");
                            }
                            break;

                        case 5:
                            flag = 1;
                            bool is_PasswordReset = false;
                            while (!is_PasswordReset)
                            {
                                Console.WriteLine("Enter Old Password");
                                string old_Password = Console.ReadLine();
                                is_PasswordReset = UpdatePassword(user_Id, old_Password);
                            }
                            break;

                        case 6:
                            flag = 1;
                            tweetappDao data5 = new tweetappDao();
                            data5.UserStatus(user_Id, false, DateTime.Now, DateTime.Now);
                            HomePage();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n----------------------------------------------\n");
                }
            }
        }

        /// <summary>
        /// forgot and reset password
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="old_password"></param>
        /// <returns></returns>
        public static bool UpdatePassword(string user_id, string old_password)
        {
            string new_password;
            string confirm_password;
            bool is_PasswordUpdate = false;
            bool is_UserAvailable;
            try
            {
                Console.WriteLine("Enter New Password");
                new_password = Console.ReadLine();
                Console.WriteLine("Confrim Password");
                confirm_password = Console.ReadLine();
                if (!old_password.Equals(new_password))
                {
                    if (new_password.Equals(confirm_password))
                    {
                        tweetappDao data = new tweetappDao();
                        is_UserAvailable = data.CheckUser(user_id, old_password);
                        if (is_UserAvailable)
                        {
                            is_PasswordUpdate = data.UpdatePassword(user_id, new_password);
                            if (is_PasswordUpdate)
                            {
                                Console.WriteLine("Password Updated Sucessfully\n----------------------------------------------\n");
                                UserLogin();
                            }
                            else
                            {
                                Console.WriteLine("Please give valid Details\n----------------------------------------------\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("User Are Not Available\n----------------------------------------------\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Password is Mismatch Or Empty\n----------------------------------------------\n");
                    }
                }
                else
                {
                    Console.WriteLine("Old and New Paswords are Same\n----------------------------------------------\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n----------------------------------------------\n");
            }
            return is_PasswordUpdate;
        }
    }
}
