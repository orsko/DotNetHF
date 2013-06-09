using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Facebook;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using Application = System.Windows.Forms.Application;

namespace DotNetHF.WinForms
{
    public partial class MainForm : Form
    {
        private int myPoints = 0;

        private QuestionItem currentQuestion ;

        private bool _validData;

        private List<QuestionItem> questions;

        private long _facebookId;
        private string _firstName;
        private string _lastName;
        private string _email;

        private bool _fbAuthComplete = false;
        private bool _loadDataComplete = false;

        public delegate void InvokeDelegate();

        public MainForm()
        {
            InitializeComponent();
            _fbAuthComplete = false;
            splitContainer1.Panel2.Visible = false;
            splitContainer1.Panel1.Visible = false;
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            FacebookLogin Fb = new FacebookLogin(this);
            Fb.Show();
            Hide();
        }

        protected override void OnActivated(EventArgs e)
        {            
            if (!_fbAuthComplete && FacebookLogin.AccessToken != null)
            {
                toolStripProgressBar1.Maximum = 100;
                toolStripProgressBar1.Minimum = 0;
                toolStripProgressBar1.Step = 50;
                toolStripProgressBar1.Increment(75);
                fbButton1.Enabled = false;
                Task.Factory.StartNew(GetFacebookAccess);
            }

            if (!_loadDataComplete)
            {
                toolStripProgressBar2.Maximum = 100;
                toolStripProgressBar2.Minimum = 0;
                toolStripProgressBar2.Step = 50;
                LoadDataBase();
            }
        }

        private void GetFacebookAccess()
        {
            try
            {
                var client = new FacebookClient();
                // Get the user's profile information
                dynamic me = client.Get("/me",
                              new
                              {
                                  fields = "first_name,last_name,email",
                                  access_token = FacebookLogin.AccessToken
                              });

                // Read the Facebook user values
                _facebookId = Convert.ToInt64(me.id);
                _firstName = me.first_name;
                _lastName = me.last_name;
                _email = me.email;

                NameLbl.BeginInvoke(new InvokeDelegate(WriteToLabel));
                _fbAuthComplete = true;
            }
            catch (Exception ex)
            {
                _fbAuthComplete = true;
                MessageBox.Show("Hiba történt :(");
            }
        }

        private void WriteToLabel()
        {
            NameLbl.Text = "Hello " + _firstName + "!";
            //toolStripProgressBar1.Increment(50);
            toolStripProgressBar1.Dispose();
            splitContainer1.Panel1.Visible = true;
        }

        private async void LoadDataBase()
        {
            try
            {
                var url = "http://localhost:55328/QuestionService.asmx/GetQuestions";
                var req = WebRequest.Create(url);
                req.Method = "POST";               
                // Create POST data and convert it to a byte array.
                string postData = "";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                req.ContentType = "application/xml";
                // Set the ContentLength property of the WebRequest.
                req.ContentLength = byteArray.Length;
                IncrementProgressBar(20);
                var res = await req.GetResponseAsync();
                IncrementProgressBar(10);
                // Get the request stream.
                var stream = res.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd().Replace("&gt;", ">").Replace("&lt;", "<").Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>","").Replace("<string xmlns=\"http://tempuri.org/\">","").Replace("</string>","").Remove(0,2).Replace("utf-16","utf-8");//.Replace("<questions xmlns=\"urn:question-schema\">","").Replace("</questions>","");
                _loadDataComplete = true;
                //MessageBox.Show(responseFromServer);                
                var ser = new XmlSerializer(typeof(List<QuestionItem>));
                MemoryStream s = new MemoryStream();
                StreamWriter writer = new StreamWriter(s);
                writer.Write(responseFromServer);
                writer.Flush();
                s.Position = 0;
                //ellenőrzés
                ValidateXml(s);
                //ha helyes               
                if (_validData)
                {
                    s.Position = 0;
                    questions = (List<QuestionItem>) ser.Deserialize(s);
                }
                reader.Close();
                stream.Close();
                res.Close();
                IncrementProgressBar(70);
                listViewQuestions.Items.Clear();
                foreach (var questionItem in questions)
                {
                    listViewQuestions.Items.Add(questionItem.Question);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ValidateXml(MemoryStream xmlStream)
        {
            try
            {
                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.Schemas.Add(null, "ArrayOfQuestionItem.xsd");
                readerSettings.ValidationType = ValidationType.Schema;
                readerSettings.ValidationEventHandler += readerSettings_ValidationEventHandler;
                using (XmlReader reader = XmlReader.Create(xmlStream, readerSettings))
                {
                    while (reader.Read())
                    {
                        //
                    }
                }
                _validData = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _validData = false;
            }
        }

        void readerSettings_ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            _validData = false;
            MessageBox.Show(e.Message);
        }

        private void IncrementProgressBar(int i)
        {
            toolStripProgressBar2.Increment(i);
        }

        public class QuestionItem
        {
            public int Id { get; set; }
            public string Question { get; set; }
            public string Answer1 { get; set; }
            public string Answer2 { get; set; }
            public string Answer3 { get; set; }
            public string Answer4 { get; set; }
            public string RightAnswer { get; set; }
            public DateTime Date { get; set; }
            public string Position { get; set; }
            public string Image { get; set; }
            public int CityId { get; set; }
        }

        private void btnAsk_Click(object sender, EventArgs e)
        {

            string stringItem = listViewQuestions.SelectedItems[0].Text;
            var q = questions.Find(a => a.Question.Equals(stringItem));
            currentQuestion = q;
            label1.Text = q.Question;
            pictureBox1.Load(q.Image);
            button1.Text = q.Answer1;
            button2.Text = q.Answer2;
            button3.Text = q.Answer3;
            button4.Text = q.Answer4;

            splitContainer1.Panel2.Visible = true;
        }


        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Workbook wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            int i;
            excel.Cells[1, 1].Value = "Kérdés";
            excel.Cells[1, 2].Value = "Dátum";
            excel.Cells[1, 3].Value = "Válasz1";
            excel.Cells[1, 4].Value = "Válasz2";
            excel.Cells[1, 5].Value = "Válasz3";
            excel.Cells[1, 6].Value = "Válasz4";

            excel.Cells[1, 8].Value = "Pontszámom:";
            excel.Cells[1, 9].Value = myPoints.ToString();

            for (i = 1; i < questions.Count; i++)
            {
                excel.Cells[i + 1, 1].Value = questions[i].Question;//nincs cast
                excel.Cells[i + 1, 2].Value = questions[i].Date;    //nincs cast
                excel.Cells[i + 1, 3].Value = questions[i].Answer1; //nincs cast
                excel.Cells[i + 1, 4].Value = questions[i].Answer2; //nincs cast
                excel.Cells[i + 1, 5].Value = questions[i].Answer3; //nincs cast
                excel.Cells[i + 1, 6].Value = questions[i].Answer4; //nincs cast
            }
            Range rAvg = excel.Cells[i + 1, 1]; //nincs cast
            //rAvg.FormulaLocal = string.Format("=ÁTLAG(A1:A{0})", i);
            //double avg = rAvg.Value; //dynamic    
            //wb.SaveAs(@"d:\Users\PersonStats.xlsx"); //lerövidült, opc. paraméterek miatt
            //excel.Quit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals(currentQuestion.RightAnswer))
            {
                MessageBox.Show("Helyes válasz!");
                Answer();
                splitContainer1.Panel2.Visible = false;
            }
            else
            {
                MessageBox.Show("Hibás válasz!");
            }                    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.Equals(currentQuestion.RightAnswer))
            {
                MessageBox.Show("Helyes válasz!");
                Answer();
                splitContainer1.Panel2.Visible = false;
            }
            else
            {
                MessageBox.Show("Hibás válasz!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text.Equals(currentQuestion.RightAnswer))
            {
                MessageBox.Show("Helyes válasz!");
                Answer();
                splitContainer1.Panel2.Visible = false;
            }
            else
            {
                MessageBox.Show("Hibás válasz!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text.Equals(currentQuestion.RightAnswer))
            {
                MessageBox.Show("Helyes válasz!");
                Answer();
                splitContainer1.Panel2.Visible = false;
            }
            else
            {
                MessageBox.Show("Hibás válasz!");
            }
        }

        class Post
        {
            public string param { get; set; }
        }

        private async void Answer()
        {
            var url = "http://localhost:55328/QuestionService.asmx/AnswerQuestion";
            var req = WebRequest.Create(url);
            req.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = "param="+_facebookId+"|"+currentQuestion.Id.ToString();
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            req.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            req.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = req.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            var res = await req.GetResponseAsync();
            // Get the request stream.
            var stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            // Read the content.
            string responseFromServer =
                reader.ReadToEnd()
                      .Replace("&gt;", ">")
                      .Replace("&lt;", "<")
                      .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "")
                      .Replace("<string xmlns=\"http://tempuri.org/\">", "")
                      .Replace("</string>", "")
                      .Remove(0, 2)
                      .Replace("utf-16", "utf-8");
            //.Replace("<questions xmlns=\"urn:question-schema\">","").Replace("</questions>","");

            myPoints = int.Parse(responseFromServer);
        }
    }
}
