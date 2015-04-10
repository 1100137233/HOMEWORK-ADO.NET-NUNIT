using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Tests
{
    [TestFixture]
    public class Class1
    {
        string ID = "";

        [TestFixtureSetUp]
        public void Initial()
        {
            string sql = @"INSERT INTO Students(Name,addr,gender)
                             VALUES
                               (@Name
                               ,@addr,
                                @gender);";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@Name", "chen"));
                    cmd.Parameters.Add(new SqlParameter("@addr", "彰化"));
                    cmd.Parameters.Add(new SqlParameter("@gender", "女"));
                    

                    ID = cmd.ExecuteScalar().ToString();

                    Console.WriteLine(ID);
                }
            }
        }

        [Test]
        public void TestAdd()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "select Max(sID) from Studnets";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while ((dr.Read()))
                        {
                            Assert.AreEqual(ID, dr.GetSqlInt32(0).ToString());
                            break;
                        }
                        dr.Close();
                    }
                }
            }
        }
    }
}
