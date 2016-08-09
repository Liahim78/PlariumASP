using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlariumEx
{
    class YTable
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyDB.mdf;Integrated Security=True";
        //Update UserBlock if UserBlock is Null. If there are other UserBlock forbiden access
        public static bool SelectAll(string userName, out string userBlock)
        {
            string commandString = "UPDATE TableY SET UserBlock=@UserBlock WHERE UserBlock is Null";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("UserBlock", userName);
                cmd.ExecuteNonQuery();
            }
            commandString = "SELECt UserBlock FROM TableY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userBlock = reader.GetString(0);
                    if (userBlock != userName)
                        return false;
                }
            }
            userBlock = "";
            return true;

        }
        //Select Row by Id and lock it
        internal static bool SelectId(string userName,int id, out string userBlock)
        {
            string commandString = "UPDATE TableY SET UserBlock=@UserBlock WHERE UserBlock is Null AND Id=@Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("UserBlock", userName);
                cmd.Parameters.AddWithValue("Id", id);
                cmd.ExecuteNonQuery();
            }
            commandString = "SELECt UserBlock FROM TableY WHERE Id=@Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Id", id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userBlock = reader.GetString(0);
                    if (userBlock != userName)
                        return false;
                }
            }
            userBlock = "";
            return true;

        }
        //Insert Row in Table
        internal static void InsertY(DataRow yRow)
        {
            string commandString = "INSERT TableY " +
                                   "VALUES (@Parametr1, @Parametr2, @UserBlock) SELECT Id FROM TableY WHERE Id = @@IDENTITY";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Parametr1", yRow[1]);
                cmd.Parameters.AddWithValue("Parametr2", yRow[2]);
                cmd.Parameters.AddWithValue("UserBlock", yRow[3]);
                try
                {
                    yRow.Table.Columns[0].ReadOnly = false;
                    yRow.SetField<int>(0, (int)cmd.ExecuteScalar());
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    yRow.Table.Columns[0].ReadOnly = true;
                }
                yRow.AcceptChanges();
            }
        }
        //Delete Row from Table
        internal static void DeleteY(DataRow yRow)
        {
            string commandString = "DELETE TableY " +
                                   "WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Id", yRow[0]);
                cmd.ExecuteNonQuery();
            }
            yRow.AcceptChanges();
        }
        //Change row
        internal static void ChangeY(DataRow yRow)
        {
            string commandString = "UPDATE TableY " +
                                   "SET Parametr1 = @Parametr1," +
                                   "Parametr2 = @Parametr2 WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Id", yRow[0]);
                cmd.Parameters.AddWithValue("Parametr1", yRow[1]);
                cmd.Parameters.AddWithValue("Parametr2", yRow[2]);
                cmd.ExecuteNonQuery();
            }
            yRow.AcceptChanges();
        }
        //Delock
        public static void Delock(string userName)
        {
            string commandString = "UPDATE TableY SET UserBlock=NULL WHERE UserBlock=@UserBlock";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("UserBlock", userName);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
