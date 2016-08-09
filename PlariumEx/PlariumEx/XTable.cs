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
        private static Dictionary<int, DateTime?> timeReadingRows;
        
        public static Dictionary<int, DateTime?> TimeReadingRows
        {   
            get { return timeReadingRows; }
            set { timeReadingRows = value; }
        }

        static MyDB myDB = new MyDB();
        static List<DataRow> listDataRow;
        static XTable()
        {
            listDataRow = new List<DataRow>();
            timeReadingRows = new Dictionary<int, DateTime?>();
        }

        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyDB.mdf;Integrated Security=True";
        public static void SelectAll()
        {
            string commandString = "SELECT Id, TimeChange FROM TableX";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        timeReadingRows.Add(reader.GetInt32(0), reader.GetDateTime(1));
                    }
                    catch (ArgumentException)
                    {
                        timeReadingRows[reader.GetInt32(0)] = reader.GetDateTime(1);
                    }
                }
            }
            
        }
        public static void SelectId(int id)
        {
            string commandString = "SELECT TimeChange FROM TableX WHERE Id=@Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Id", id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        timeReadingRows.Add(id, reader.GetDateTime(0));
                    }
                    catch(ArgumentException)
                    {
                        timeReadingRows[id] = reader.GetDateTime(0);
                    }
                }
            }
        }
        public static void InsertX(DataRow XRow)
        {
            listDataRow.Add(XRow);
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
            listDataRow.Add(XRow);
            
            string commandString = "UPDATE TableX " +
                                   "SET Parametr1 = @Parametr1," +
                                   "Parametr2 = @Parametr2," +
                                   "TimeChange = @TimeChange WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Id", XRow[0]);
                cmd.Parameters.AddWithValue("Parametr1", XRow[1]);
                cmd.Parameters.AddWithValue("Parametr2", XRow[2]);
                cmd.Parameters.AddWithValue("TimeChange", XRow[3]);
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteX(DataRow XRow)
        {
            listDataRow.Add(XRow);
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
        public static void Save()
        {
            foreach (var item in listDataRow)
            {
                item.AcceptChanges();  
            }
        }
    }
}
