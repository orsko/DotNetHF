using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
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

        private const string CONNSTR =
            @"Data Source=tcp:t0isor0qkh.database.windows.net,1433;Initial Catalog=questions;Persist Security Info=True;User ID=onlab;Password=AUTdatabase1";

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

                    var questions = new List<QuestionItem>();

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

                        int Id = reader.GetInt32(9);

                        int cityId = reader.GetInt32(10);

                        questions.Add(new QuestionItem
                            {
                                Id = Id,
                                Answer1 = answer1,
                                Answer2 = answer2,
                                Answer3 = answer3,
                                Answer4 = answer4,
                                RightAnswer = rightanswer,
                                CityId = cityId,
                                Date = date,
                                Image = image,
                                Position = position,
                                Question = question
                            });
                    }

                    ////XML összeállítása. listában a kérdések
                    //XNamespace ns = "urn:question-schema";
                    //var xDoc = new XDocument(
                    //    new XDeclaration("1.0", "UTF-8", "yes"),
                    //    new XElement(ns + "questions",
                    //        from q in questions
                    //        select new XElement(ns + "item",
                    //                 new XElement(ns + "question", q.Question),
                    //                 new XElement(ns + "answers",
                    //                              new XElement(ns + "answer1", q.Answer1),
                    //                              new XElement(ns + "answer2", q.Answer2),
                    //                              new XElement(ns + "answer3", q.Answer3),
                    //                              new XElement(ns + "answer4", q.Answer4),
                    //                              new XElement(ns + "rightanswer", q.RightAnswer)),
                    //                 new XElement(ns + "position", q.Position),
                    //                 new XElement(ns + "city", q.CityId),
                    //                 new XElement(ns + "image", q.Image)
                    //                 )
                    //        )
                    //    );

                    var ser = new XmlSerializer(typeof (List<QuestionItem>));
                    TextWriter textWriter = new StringWriter();

                    ser.Serialize(textWriter, questions);

                    var ret = textWriter.ToString();

                    return ret;

                    //var sb = new StringBuilder();
                    //var settings = new XmlWriterSettings
                    //    {
                    //        Encoding = Encoding.UTF8,
                    //        ConformanceLevel = ConformanceLevel.Document,
                    //        Indent = true
                    //    };
                    //using (XmlWriter writer = XmlWriter.Create(sb, settings))
                    //{
                    //    xDoc.Save(writer);
                    //}

                    //return sb.ToString().Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");

                }
            }
        }

        [WebMethod]
        public string AnswerQuestion(string param)
        {
            string userName = param.Split('|')[0];
            int questionId = int.Parse(param.Split('|')[1]);
            using (var conn = new SqlConnection(CONNSTR))
            {
                StringBuilder response = new StringBuilder();

                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                var players = new List<Player>();

                command.CommandText = @"SELECT * FROM [Players]";

                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            players.Add(new Player
                                {
                                    Id = reader.GetInt32(0),
                                    UserName = reader.GetString(1),
                                    Points = reader.GetInt32(2)
                                });
                        }
                }

                var player = players.Find(p => p.UserName.Equals(userName));
                if (player == null)
                {
                    player = new Player
                        {
                            UserName = userName,
                            Points = 0
                        };
                    command = new SqlCommand();
                    command.Connection = conn;

                    command.CommandText = "INSERT INTO [Players] (UserName, Points) VALUES (@username , @points)";

                    command.Parameters.Add("@username", SqlDbType.Text);
                    command.Parameters["@username"].Value = player.UserName;

                    command.Parameters.AddWithValue("@points", player.Points);

                    command.ExecuteNonQuery();

                    command = new SqlCommand();
                    command.Connection = conn;

                    command.CommandText = @"SELECT TOP 1 ID FROM [Players] WHERE UserName LIKE @username";
                    command.Parameters.AddWithValue("@username", player.UserName);

                    player.Id = (int)command.ExecuteScalar();
                }

                command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"SELECT QuestionId FROM [PlayerToQuestion] WHERE PlayerID=@id";
                command.Parameters.AddWithValue("@id", player.Id);

                var myQuestions = new List<int>();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            myQuestions.Add(reader.GetInt32(0));
                        }
                    }
                }

                if (myQuestions.Contains(questionId))
                {
                    return "Mar megvalaszolt kerdes";
                }

                command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"INSERT INTO [PlayerToQuestion] (PlayerId, QuestionId) VALUES (@pid, @qid)";

                command.Parameters.AddWithValue("@pid", player.Id);
                command.Parameters.AddWithValue("@qid", questionId);

                command.ExecuteNonQuery();

                command = new SqlCommand();
                command.Connection = conn;

                command.CommandText = @"UPDATE [Players] SET Points = @points WHERE ID = @id";

                command.Parameters.AddWithValue("@points", (player.Points + 1));
                command.Parameters.AddWithValue("@id", player.Id);

                command.ExecuteNonQuery();


                return (player.Points+1).ToString();
            }
        }
    }
}
