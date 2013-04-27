using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace DotNetHF
{
    public partial class Questions : System.Web.UI.Page
    {
        private const string CONNSTR = @"Data Source=tcp:t0isor0qkh.database.windows.net,1433;Initial Catalog=questions;Persist Security Info=True;User ID=onlab;Password=AUTdatabase1";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    //LINQ 2 SQL elemek lekérése az xml-ből
                    XDocument xDoc = XDocument.Load(FileUpload1.FileContent);
                    XNamespace ns = "urn:question-schema";
                    string question = xDoc.Descendants(ns + "question").FirstOrDefault().Value.ToString();

                    string answer1 = xDoc.Descendants(ns + "answer1").FirstOrDefault().Value.ToString();
                    string answer2 = xDoc.Descendants(ns + "answer2").FirstOrDefault().Value.ToString();
                    string answer3 = xDoc.Descendants(ns + "answer3").FirstOrDefault().Value.ToString();
                    string answer4 = xDoc.Descendants(ns + "answer4").FirstOrDefault().Value.ToString();
                    string rightanswer = xDoc.Descendants(ns + "rightanswer").FirstOrDefault().Value.ToString();

                    string position = xDoc.Descendants(ns + "position").FirstOrDefault().Value.ToString();
                    string image = xDoc.Descendants(ns + "image").FirstOrDefault().Value.ToString();
                    int cityId = int.Parse(xDoc.Descendants(ns + "city").FirstOrDefault().Value.ToString());

                    // adatok betöltése az adatbázisba
                    using (var conn = new SqlConnection(CONNSTR))
                    {
                        SqlCommand command = new SqlCommand();
                        command.Connection = conn;
                        command.CommandText =
                            @"INSERT INTO [Questions] ([Question], [Date], [Position], [Answer1], [Answer2], [Answer3], [Answer4], [RightAnswer], [Image], [CityID]) VALUES (@Question, @Date, @Position, @Answer1, @Answer2, @Answer3, @Answer4, @RightAnswer, @Image, @CityID)";
                        command.Parameters.AddWithValue("@Question", question);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);
                        command.Parameters.AddWithValue("@Position", position);
                        command.Parameters.AddWithValue("@Answer1", answer1);
                        command.Parameters.AddWithValue("@Answer2", answer2);
                        command.Parameters.AddWithValue("@Answer3", answer3);
                        command.Parameters.AddWithValue("@Answer4", answer4);
                        command.Parameters.AddWithValue("@RightAnswer", rightanswer);
                        command.Parameters.AddWithValue("@Image", image);
                        command.Parameters.AddWithValue("@CityID", cityId);
                        conn.Open();
                        int num = command.ExecuteNonQuery();
                        conn.Close();
                        if (num > 0)
                        {
                            num = 0;
                        }
                    }
                    // Oldal újratöltése
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    Response.Write("<script>alert('Nincs megadva fájl')</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Nem megfelelő fájl')</script>");
            }
        }
    }
}