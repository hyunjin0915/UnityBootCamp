using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace MySQLClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //login
            string connectionString = "server=localhost;user=root;database=membership;password=hj0428!!";

            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.CommandText = "select * from users where user_id = @user_id and user_password = @user_password";
                mySqlCommand.Prepare();
                mySqlCommand.Parameters.AddWithValue("@user_id", "toothless");
                mySqlCommand.Parameters.AddWithValue("@user_password", "1234");

                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader["name"] + " " + dataReader["email"]);
                }

                MySqlCommand mySqlCommand2 = new MySqlCommand();
                mySqlCommand2.Connection = mySqlConnection;

                mySqlCommand2.CommandText = "insert into users (user_id, user_password, name, email) values ( @user_id, @user_password, @name, @email)";
                mySqlCommand2.Prepare();
                mySqlCommand2.Parameters.AddWithValue("@user_id", "hambugi");
                mySqlCommand2.Parameters.AddWithValue("@user_password", "1234");
                mySqlCommand2.Parameters.AddWithValue("@name", "bugibugi");
                mySqlCommand2.Parameters.AddWithValue("@email", "bugi@gmail.com");
                mySqlCommand2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }
    }
}
