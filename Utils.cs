using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Staff_Management_App
{
    internal class Utils
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\DB_Staff.mdf;Integrated Security=True;Connect Timeout=30");

        public void AddadmintoDB(Admin admin)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Insert into [Admins] (fname,lname,pin,role) values('" + admin.fName + "','" + admin.lName + "',Hashbytes('SHA2_512','" + admin.pin + "'),'"+ admin.role + "')";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        /*
         * static string HashString(string text, string salt = "")
{
    if (String.IsNullOrEmpty(text))
    {
        return String.Empty;
    }
    
    // Uses SHA256 to create the hash
    using (var sha = new System.Security.Cryptography.SHA256Managed())
    {
        // Convert the string to a byte array first, to be processed
        byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
        byte[] hashBytes = sha.ComputeHash(textBytes);
        
        // Convert back to a string, removing the '-' that BitConverter adds
        string hash = BitConverter
            .ToString(hashBytes)
            .Replace("-", String.Empty);

        return hash;
    }
}
         * */

        //working
        public void AddmessagetoInbox(int id, string message)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            //Getting the timestamp and including it in the SQL...

            DateTime timeStamp = DateTime.Now;

           cmd.CommandText = "Insert into [Inbox] (ID,Messages,Timestamp) values(" + id +",'"+ message +"','"+ timeStamp+"')";
           cmd.ExecuteNonQuery();
           connection.Close();
        }

        //working
        public void ReadInbox(int id)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            //Getting the messages using the id given
            cmd.CommandText = "SELECT * FROM [Inbox] WHERE Id=" + id;
           
            string message;
            int sn ;

            //To get multiple rows from the DB and read it out
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    sn = reader.GetInt32(0);
                    message = reader.GetString(1);
                    Console.WriteLine("" + sn + " : " + message);
                }
            }
            connection.Close();
        }
        //working
        public void ClearInbox(int id)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            //Getting the messages using the id given
            cmd.CommandText = "DELETE FROM [Inbox] WHERE Id=" + id;
            cmd.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Messages successfully deleted...");
        }
    }
}
