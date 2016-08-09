using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlariumEx
{
    public partial class YSiteAsp : System.Web.UI.Page
    {
        MyDB myDB = new MyDB();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        //Select all Rows from TableY
        protected void SelectAllXButton_Click(object sender, EventArgs e)
        {
            string userBlock;
            if (!YTable.SelectAll(UserNameLabel.Text, out userBlock))
            {
                Response.Write("<script> alert(\"Данные заблокированы пользователем - "+userBlock+"\"); </script>");
                return;
            }
            TableY.DataSourceID = "YSqlDataSource";
            TableY.Visible = true;
            IdYTextBox.Visible = LabelPrY1.Visible = LabelPrY2.Visible = PrY1TextBox.Visible = PrY2TextBox.Visible = AddYButton.Visible = UpdateYButton.Visible = DeleteYButton.Visible = true;
        }
        //Delock 
        protected void DeblockButton_Click(object sender, EventArgs e)
        {
            YTable.Delock(UserNameLabel.Text);
            TableY.Visible = false;
            IdYTextBox.Visible = LabelPrY1.Visible = LabelPrY2.Visible = PrY1TextBox.Visible = PrY2TextBox.Visible = AddYButton.Visible = UpdateYButton.Visible = DeleteYButton.Visible = false;

        }
        //Select by Id
        protected void SelectByIdXButton_Click(object sender, EventArgs e)
        {
            string userBlock;
            if (!YTable.SelectId(UserNameLabel.Text, Convert.ToInt32(IdYTextBox.Text), out userBlock))
            {
                Response.Write("<script> alert(\"Данные заблокированы пользователем - " + userBlock + "\"); </script>");
                return;
            }
            TableY.DataSourceID = "YIdSqlDataSource";
            TableY.Visible = true;
            IdYTextBox.Visible = LabelPrY1.Visible = LabelPrY2.Visible = PrY1TextBox.Visible = PrY2TextBox.Visible = AddYButton.Visible = UpdateYButton.Visible = DeleteYButton.Visible = true;

        }
        //Add to TableY
        protected void AddXButton_Click(object sender, EventArgs e)
        {
            DataRow YRow = myDB.TableY.NewRow();
            YRow["Parametr1"] = PrY1TextBox.Text;
            YRow["Parametr2"] = PrY2TextBox.Text;
            YRow["UserBlock"] = UserNameLabel.Text;
            myDB.TableY.Rows.Add(YRow);
            YTable.InsertY(YRow);
            TableY.DataSourceID = TableY.DataSourceID;
        }
        //Update Row in TableY
        protected void UpdateXButton_Click(object sender, EventArgs e)
        {
            DataRow YRow = myDB.TableY.NewRow();
            YRow["Id"] = (int)TableY.SelectedDataKey.Values["Id"];
            YRow["Parametr1"] = PrY1TextBox.Text;
            YRow["Parametr2"] = PrY2TextBox.Text;
            myDB.TableY.Rows.Add(YRow);
            YTable.ChangeY(YRow);
            TableY.DataSourceID = TableY.DataSourceID;
        }
        //Delete Row in TableY
        protected void DeleteXButton_Click(object sender, EventArgs e)
        {
            DataRow YRow = myDB.TableY.NewRow();
            YRow["Id"] = (int)TableY.SelectedDataKey.Values["Id"];
            myDB.TableY.Rows.Add(YRow);
            YTable.DeleteY(YRow);
            TableY.DataSourceID = TableY.DataSourceID;
        }
        //Write in textBox from selectRow
        protected void TableY_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectIdLabel.Text = string.Format("Select Id = {0}", (int)TableY.SelectedDataKey.Values["Id"]);
            PrY1TextBox.Text = TableY.SelectedRow.Cells[2].Text;
            PrY2TextBox.Text = TableY.SelectedRow.Cells[3].Text;
        }
        //Log in and write UserName
        protected void LogInButton_Click(object sender, EventArgs e)
        {
            UserNameLabel.Text = UserNameTextBox.Text;
            UserNameTextBox.Visible = LogInButton.Visible = false;
            SelectAllYButton.Visible = SelectByIdYButton.Visible = DeblockButton.Visible = IdYTextBox.Visible = true;
        }
    }
}