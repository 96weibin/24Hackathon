using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionList
{
    public partial class Summary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public List<QuestionSummary> getAllQuestion()
        {
            string connStr = "server=tcp:hackathon-aichat-dbserver.database.windows.net,1433;database=KnowledgeBase;uid=hackathonsa;pwd=Aspen000";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = "select * from Questions";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            List<QuestionSummary> data = new List<QuestionSummary>();
            while (rd.Read())
            {
                data.Add(new QuestionSummary()
                {
                    Title = rd["Title"].ToString(),
                    Summary = rd["Summary"].ToString()
                });
            }
            //this.CustomersGridView.DataSource = data;
            conn.Close();
            return data;
        }

        public class QuestionSummary
        {
            public string Title { get; set; }
            public string Summary { get; set; }
        }
    }
}