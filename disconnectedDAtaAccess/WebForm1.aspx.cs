using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace disconnectedDAtaAccess
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void getDAtaFromDb()
        {
            SqlConnection con = new SqlConnection("Data Source=PRADEEP\\SQLEXPRESS;Initial catalog=Students;Integrated Security=true");
            String strSelectCmd = "select * from tblStudents";
            SqlDataAdapter da = new SqlDataAdapter(strSelectCmd, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "Students");

            ds.Tables["Students"].PrimaryKey = new DataColumn[] { ds.Tables["Students"].Columns["Id"] };
            Cache.Insert("Dataset", ds, null, System.DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);

            GridView1.DataSource = ds;
            GridView1.DataBind();

            Label1.Text = "Data Loaded from DataBase";

        }

        private void getDataFromCache()
        {
            if (Cache["Dataset"] != null)
            {
                DataSet ds = (DataSet)Cache["Dataset"];
                GridView1.DataSource = ds;
                GridView1.DataBind();




            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            getDAtaFromDb();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            getDataFromCache();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if(Cache["Dataset"]!=null)
            {
                DataSet ds =  (DataSet)Cache["Dataset"];
                DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["Id"]);

               dr["Name"]= e.NewValues["Name"];
                dr["Gender"] = e.NewValues["Gender"];
                dr["TotalMarks"] = e.NewValues["TotalMarks"];

                Cache.Insert("Dataset", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                GridView1.EditIndex = -1;
                getDataFromCache();

            }

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex =-1;
            getDataFromCache();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Cache["Dataset"] != null)
            {
                DataSet ds = (DataSet)Cache["Dataset"];
                DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["Id"]);

                dr.Delete();

                Cache.Insert("Dataset", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
               
                getDataFromCache();

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=PRADEEP\\SQLEXPRESS;Initial catalog=Students;Integrated Security=true");
            String strSelectCmd = "select * from tblStudents";
            SqlDataAdapter da = new SqlDataAdapter(strSelectCmd, con);

            DataSet ds=(DataSet)Cache["Dataset"];

            String strUpdateCmd = "update tblStudents set Name=@Name, Gender=@Gender, TotalMarks=@TotalMarks where Id=@Id";
            SqlCommand updateCommand = new SqlCommand(strUpdateCmd, con);
            updateCommand.Parameters.Add("@Name", SqlDbType.VarChar, 50, "Name");
            updateCommand.Parameters.Add("@Gender", SqlDbType.VarChar, 10, "Gender");
            updateCommand.Parameters.Add("@TotalMarks", SqlDbType.Int, 0, "TotalMarks");
            updateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

            da.UpdateCommand = updateCommand;

            String strDeleteCmd = "delete from tblStudents where Id=@Id";
            SqlCommand deleteCommand = new SqlCommand(strDeleteCmd, con);
            deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

            da.DeleteCommand = deleteCommand;

            da.Update(ds, "Students");

            Label1.Text = "Database Table Updated";

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["Dataset"];
            if(ds.HasChanges())
            {
                ds.RejectChanges();
                Cache.Insert("Dataset", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                getDataFromCache();
                Label1.Text = "Changes Undone";
                Label1.ForeColor = System.Drawing.Color.Green;
            }
            else
            {

                Label1.Text = "No changes made";
                Label1.ForeColor = System.Drawing.Color.Red;
            }





            /* for status of each row
            foreach(DataRow dr in ds.Tables["Students"].Rows)
            {
                if(dr.RowState==DataRowState.Deleted)
                {
                    Response.Write(dr["Id",DataRowVersion.Original].ToString() + "-" + dr.RowState.ToString() + "<br/>");
                }

                else{
                    Response.Write(dr["Id"].ToString() + "-" + dr.RowState.ToString() + "<br/>");
                }
            }*/
        }
    }
}