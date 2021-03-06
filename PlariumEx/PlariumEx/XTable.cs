﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
        
        static XTable()
        {
            timeReadingRows = new Dictionary<int, DateTime?>();
        }

        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyDB.mdf;Integrated Security=True";
        //write in timeReadingRow TimeChange from TableX
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
                    try // if there isn't timeReadingRow with key=reader.GetInt32(0)
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
        //write TimeChange from TableX where id = SelectedId in timeReadingRow
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
                    try// if there isn't timeReadingRow with key=id
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
        //insert XRow in TableX
        public static void InsertX(DataRow xRow)
        {
            string commandString = "INSERT TableX " +
                                   "VALUES (@Parametr1, @Parametr2, @TimeChange) SELECT Id FROM TableX WHERE Id = @@IDENTITY";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Parametr1", xRow[1]);
                cmd.Parameters.AddWithValue("Parametr2", xRow[2]);
                cmd.Parameters.AddWithValue("TimeChange", xRow[3]);
                try
                {
                    xRow.Table.Columns[0].ReadOnly = false;
                    xRow.SetField<int>(0, (int)cmd.ExecuteScalar());
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    xRow.Table.Columns[0].ReadOnly = true;
                }
                xRow.AcceptChanges();
            }
        }
        //Change XRow in TableX
        public static void ChangeX(DataRow xRow)
        {
            
            string commandString = "UPDATE TableX " +
                                   "SET Parametr1 = @Parametr1," +
                                   "Parametr2 = @Parametr2," +
                                   "TimeChange = @TimeChange WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Id", xRow[0]);
                cmd.Parameters.AddWithValue("Parametr1", xRow[1]);
                cmd.Parameters.AddWithValue("Parametr2", xRow[2]);
                cmd.Parameters.AddWithValue("TimeChange", xRow[3]);
                cmd.ExecuteNonQuery();
                SelectId((int)xRow[0]);
            }
            xRow.AcceptChanges();
        }
        //Delete Xrow in TableX
        public static void DeleteX(DataRow xRow)
        {
            string commandString = "DELETE TableX " +
                                   "WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.AddWithValue("Id", xRow[0]);
                cmd.ExecuteNonQuery();
            }
            xRow.AcceptChanges();
        }
        
    }
}
