using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //For SQL
using System.Security.Cryptography;

namespace Staff_Management_App
{

    internal class Utils
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\DB_Staff.mdf;Integrated Security=True;Connect Timeout=30");

        //To ensure proper handling of the SQL query and avoid SQL injection, you should use parameterized queries.
        public void AddadmintoDB(Admin admin)
        {
            connection.Open();

            // Use parameterized query
            SqlCommand cmd = new SqlCommand("INSERT INTO [Admins] (fname, lname, pin, role) VALUES (@fname, @lname, Hashbytes('SHA2_512', @pin), @role)", connection);

            // Add parameter values
            cmd.Parameters.AddWithValue("@fname", admin.fName);
            cmd.Parameters.AddWithValue("@lname", admin.lName);
            cmd.Parameters.AddWithValue("@pin", Encoding.UTF8.GetBytes(admin.pin));
            cmd.Parameters.AddWithValue("@role", admin.role);

            cmd.ExecuteNonQuery();
            connection.Close();
        }



        public bool checkAdminInDB(Admin admin)
        {
            bool status = false;
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) FROM [Admins] WHERE fname='" + admin.fName + "' AND lname= '" + admin.lName + "'";
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                status = true;
            }
           connection.Close();

            return status;
        }

        public bool VerifyPassWord(Admin admin, string password)
        {
            bool status = false;
            string hashedString;
            string hashedValue="";

            connection.Open();

            string query = "SELECT pin FROM [Admins] WHERE fname='" + admin.fName + "' AND lname= '" + admin.lName + "'";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                //Converting the pin from Binary to hexadecimal which is used to store hashed values
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    byte[] hashedBytes = (byte[])reader["pin"];
                    hashedValue = ByteArrayToHexString(hashedBytes);
                }
                reader.Close();
            }

            connection.Close();

            using (SHA512 sha512 = SHA512.Create())
            { 
                //Create an instance of SHA-512 algorithm
                //converts the string to bytes
                //Compute the hash
                byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                //Converts the hashed bytes to a string representation
                hashedString = BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
            }
          

            Console.WriteLine("Pin From DB: ");
            Console.WriteLine(hashedValue);
            Console.WriteLine("Pin From User: ");
            Console.WriteLine(hashedString);

            if(hashedValue == hashedString)
            {
                status = true;
                Console.WriteLine("Password Correct!");
                return status;
            }

            Console.WriteLine("Password Incorrect!");
            return status;
        }

        //helper function which constructs the hexadecimal characters
        static string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString().ToUpperInvariant();
        }

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
