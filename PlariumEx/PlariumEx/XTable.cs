using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlariumEx
{
    class XTable
    {
        private static DateTime? [] timeReadingRows;

        public static DateTime? [] TimeReadingRows
        {   
            get { return timeReadingRows; }
            set { timeReadingRows = value; }
        }

        static MyDB myDB = new MyDB();
        static DataTable myTable;
        static XTable()
        {
            myTable = myDB.TableX;
        }

        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyDB.mdf;Integrated Security=True";
        public static void SelectAll()
        {
            string commandString = "SELECT TimeChange FROM TableX";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                var s = cmd.ExecuteReader();
                int length = 0;
                foreach (var item in s)
                    length++;
                TimeReadingRows = new DateTime?[length];
                int i = 0;
                foreach (var item in s)
                {
                    TimeReadingRows[i++] = item as DateTime?;
                }
            }
        }
        public static void SelectId(int id)
        {
            string commandString = "SELECT TimeChange FROM TableX";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                var s = cmd.ExecuteReader();
                int length = 0;
                foreach (var item in s)
                    length++;
                TimeReadingRows = new DateTime?[length];
                TimeReadingRows[id] = s[id] as DateTime?;
            }
        }
        public static void InsertX(DataRow XRow)
        {
            myTable.Rows.Add(XRow);
            string commandString = "INSERT TableX " +
                                   "VALUES (@Parametr1, @Parametr2, @TimeChange) SELECT Id FROM TableX WHERE Id = @@IDENTITY";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Parametr1", XRow[1]);
                cmd.Parameters.AddWithValue("Parametr2", XRow[2]);
                cmd.Parameters.AddWithValue("TimeChange", XRow[3]);
                try
                {
                    XRow.Table.Columns[0].ReadOnly = false;
                    XRow.SetField<int>(0, (int)cmd.ExecuteScalar());
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    XRow.Table.Columns[0].ReadOnly = true;
                }
            }
        }
        public static void ChangeX(DataRow XRow)
        {
            myTable.Rows.Add(XRow);
            string commandString = "UPDATE TableX " +
                                   "SET Parametr1 = @Parametr1," +
                                   "Parametr2 = @Parametr2 WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Id", XRow[0]);
                cmd.Parameters.AddWithValue("Parametr1", XRow[1]);
                cmd.Parameters.AddWithValue("Parametr2", XRow[2]);
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteX(DataRow XRow)
        {
            myTable.Rows.Add(XRow);
            string commandString = "DELETE TableX " +
                                   "WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Id", XRow[0]);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
