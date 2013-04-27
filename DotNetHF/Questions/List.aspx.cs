﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        protected void XmlExportButtonAction(object sender, EventArgs e)
        {
            var s = 0;
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
            catch (Exception exc)
            {
                Response.Write("<script>alert('Nem megfelelő fájl')</script>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                var id = (int) QuestionsGridView.SelectedValue;
                // adatok lekérése az adatbázisból
                using (var conn = new SqlConnection(CONNSTR))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandText =
                        @"SELECT * FROM [Questions] WHERE [ID] = @ID";
                    command.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return;
                        }
                        string question = reader.GetString(0);
                        DateTime date = reader.GetDateTime(1);
                        string position = reader.GetString(2);
                        string answer1 = reader.GetString(3);
                        string answer2 = reader.GetString(4);
                        string answer3 = reader.GetString(5);
                        string answer4 = reader.GetString(6);
                        string rightanswer = reader.GetString(7);
                        string image = reader.GetString(8);
                        int cityId = reader.GetInt32(10);

                        conn.Close();
                        //XML összeállítása
                        XNamespace ns = "urn:question-schema";
                        var xDoc = new XDocument(
                            new XDeclaration("1.0", "UTF-8", "yes"),
                            new XElement(ns + "questiondoc",
                                         new XElement(ns + "question", question),
                                         new XElement(ns + "answers",
                                                      new XElement(ns + "answer1", answer1),
                                                      new XElement(ns + "answer2", answer2),
                                                      new XElement(ns + "answer3", answer3),
                                                      new XElement(ns + "answer4", answer4),
                                                      new XElement(ns + "rightanswer", rightanswer)),
                                         new XElement(ns + "position", position),
                                         new XElement(ns + "city", cityId),
                                         new XElement(ns + "image", image)
                                )
                            );
                        //Válasz beállítása
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentEncoding = System.Text.Encoding.UTF8;
                        Response.Charset = "UTF-8";
                        Response.AddHeader("content-disposition", "attachment;filename=question_" + id + ".xml");
                        Response.ContentType = "application/xml";
                        //writerbe csomagolva kell, mert másképp a header nem lesz ott
                        var sb = new StringBuilder();
                        var settings = new XmlWriterSettings
                            {
                                Encoding = Encoding.UTF8,
                                ConformanceLevel = ConformanceLevel.Document,
                                Indent = true
                            };
                        using (XmlWriter writer = XmlWriter.Create(sb, settings))
                        {
                            xDoc.Save(writer);
                        }
                        Response.Write(sb.ToString().Replace("encoding=\"utf-16\"", "encoding=\"utf-8\""));
                        Response.End();
                    }
                }
            }
            catch (NullReferenceException exc)
            {
                Response.Write("<script>alert('Nincs kiválasztva kérdés')</script>");
            }
        }
    }
}