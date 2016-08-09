using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlariumEx
{
    public partial class SiteAsp : System.Web.UI.Page
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyDB.mdf;Integrated Security=True";
        int indexX;
        MyDB myDB = new MyDB();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SelectAllXButton_Click(object sender, EventArgs e)
        {
            TableX.DataSourceID = "XSqlDataSource";
            XTable.SelectAll();
            TableX.Visible = true;
        }

        protected void SelectByIdXButton_Click(object sender, EventArgs e)
        {
            TableX.DataSourceID = "XIdSqlDataSource";
            TableX.Visible = true;
            IdXTextBox.Text = IdXTextBox.Text == "" ? 1.ToString() : IdXTextBox.Text ;
            XTable.SelectId(Convert.ToInt32(IdXTextBox.Text));
        }

        protected void TableX_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexX = (int)TableX.SelectedDataKey.Values["Id"];
            SelectIdLabel.Text = string.Format("Select Id = {0}", indexX);
            PrX1TextBox.Text = TableX.SelectedRow.Cells[2].Text;
            PrX2TextBox.Text = TableX.SelectedRow.Cells[3].Text;
        }

        protected void AddXButton_Click(object sender, EventArgs e)
        {
            DataRow XRow = myDB.TableX.NewRow();
            XRow["Parametr1"] = PrX1TextBox.Text;
            XRow["Parametr2"] = PrX2TextBox.Text;
            XRow["TimeChange"] = DateTime.Now;
            myDB.TableX.Rows.Add(XRow);
            XTable.InsertX(XRow);
        }

        protected void UpdateXButton_Click(object sender, EventArgs e)
        {
            string commandString = "SELECT TimeChange FROM TableX WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(commandString, connection);
                int index = (int)TableX.SelectedDataKey.Values["Id"];
                cmd.Parameters.AddWithValue("Id", index);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? myDateTime = reader.GetDateTime(0);
                    if (myDateTime != XTable.TimeReadingRows[index])
                    {
                        Response.Write("<script> alert(\"Данные были изменены обновите данные\"); </script>");
                        return;
                    }
                }
            }
            DataRow XRow = myDB.TableX.NewRow();
            XRow["Id"] = (int)TableX.SelectedDataKey.Values["Id"];
            XRow["Parametr1"] = PrX1TextBox.Text;
            XRow["Parametr2"] = PrX2TextBox.Text;
            XRow["TimeChange"] = DateTime.Now;
            myDB.TableX.Rows.Add(XRow);
            XTable.ChangeX(XRow);
        }

        protected void DeleteXButton_Click(object sender, EventArgs e)
        {
            DataRow XRow = myDB.TableX.NewRow();
            XRow["Id"] = (int)TableX.SelectedDataKey.Values["Id"];
            XRow["Parametr1"] = PrX1TextBox.Text;
            XRow["Parametr2"] = PrX2TextBox.Text;
            myDB.TableX.Rows.Add(XRow);
            XTable.DeleteX(XRow);
        }

        protected void SaveChengesXButton_Click(object sender, EventArgs e)
        {
            XTable.Save();
            TableX.Visible = false;
        }
    }
}