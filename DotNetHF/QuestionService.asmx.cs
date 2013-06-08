using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using DotNetHF.Model;

namespace DotNetHF
{
    /// <summary>
    /// Summary description for QuestionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class QuestionService : System.Web.Services.WebService
    {

        private const string CONNSTR = @"Data Source=tcp:t0isor0qkh.database.windows.net,1433;Initial Catalog=questions;Persist Security Info=True;User ID=onlab;Password=AUTdatabase1";

        [WebMethod]
        public string GetQuestions()
        {
            using (var conn = new SqlConnection(CONNSTR))
            {
                StringBuilder response = new StringBuilder();

                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT * FROM [Questions]";

                conn.Open();
                using (var reader = command.ExecuteReader())
                {

                    var quesions = new List<QuestionItem>();

                    while (reader.Read())
                    {
                        var question = reader.GetString(0);
                        var date = reader.GetDateTime(1);
                        var position = reader.GetString(2);
                        var answer1 = reader.GetString(3);
                        var answer2 = reader.GetString(4);
                        var answer3 = reader.GetString(5);
                        var answer4 = reader.GetString(6);
                        var rightanswer = reader.GetString(7);
                        string image;
                        try
                        {
                            image = reader.GetString(8);
                        }
                        catch (Exception)
                        {
                            image = "";
                        }
                        int cityId = reader.GetInt32(10);

                        quesions.Add(new QuestionItem
                            {
                                Answer1=answer1,
                                Answer2=answer2,
                                Answer3=answer3,
                                Answer4=answer4,
                                RightAnswer=rightanswer,
                                CityId=cityId,
                                Date=date,
                                Image=image,
                                Position=position,
                                Question=question
                            });
                    }

                    //XML összeállítása. listában a kérdések
                    XNamespace ns = "urn:question-schema";
                    var xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement(ns + "questions",
                            from q in quesions
                            select new XElement(ns + "item",
                                     new XElement(ns + "question", q.Question),
                                     new XElement(ns + "answers",
                                                  new XElement(ns + "answer1", q.Answer1),
                                                  new XElement(ns + "answer2", q.Answer2),
                                                  new XElement(ns + "answer3", q.Answer3),
                                                  new XElement(ns + "answer4", q.Answer4),
                                                  new XElement(ns + "rightanswer", q.RightAnswer)),
                                     new XElement(ns + "position", q.Position),
                                     new XElement(ns + "city", q.CityId),
                                     new XElement(ns + "image", q.Image)
                                     )
                            )
                        );



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

                    return sb.ToString().Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");

                }
            }
        }
    }
}
