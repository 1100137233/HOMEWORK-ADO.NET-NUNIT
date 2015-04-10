using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    /// <summary>
    /// ExecuteReader, ExecuteNonQuery, ExecuteScalar
    /// </summary>
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "select * from circle.dbo.Students where Name like @Name";
                    cmd.Parameters.Add(new SqlParameter("@Name", "%" + txtSearch.Text + "%"));
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        gvResult.DataSource = dt;
                        gvResult.DataBind();
                        dr.Close();
                    }
                }
            }
        }

        //ExecuteScalar
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO Studnets(Name,addr,gender)
                             VALUES
                               (@Name
                               ,@addr,
                                @gender);SELECT CAST(scope_identity() AS int);";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@Name", txtName.Text));
                    cmd.Parameters.Add(new SqlParameter("@addr", txtAddr.Text));
                    cmd.Parameters.Add(new SqlParameter("@gender", txtGen.Text));

                    //txtE_Id.Text = cmd.ExecuteScalar().ToString();
                }
                btnSearch_Click(null, null);
            }
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string sql = @"update [Students] set [Name] = @Name, [addr] = @addr ,[gender] = @gender,
                             where sID = @sID";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@sID", txtE_Id.Text));
                    cmd.Parameters.Add(new SqlParameter("@Name", txtE_Name.Text));
                    cmd.Parameters.Add(new SqlParameter("@addr", txtE_Addr.Text));
                    cmd.Parameters.Add(new SqlParameter("@gender", txtE_Gen.Text));

                    cmd.ExecuteNonQuery();
                }
                btnSearch_Click(null, null);
            }
        }

    }
}