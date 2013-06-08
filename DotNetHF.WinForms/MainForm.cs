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
using Facebook;

namespace DotNetHF.WinForms
{
    public partial class MainForm : Form
    {

        private long _facebookId;
        private string _firstName;
        private string _lastName;
        private string _email;

        private bool _fbAuthComplete;
        private bool _loadDataComplete;

        public delegate void InvokeDelegate();

        public MainForm()
        {
            InitializeComponent();
            _fbAuthComplete = false;
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
                LoginBtn.Enabled = false;
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
                string responseFromServer = reader.ReadToEnd().Replace("&gt;",">").Replace("&lt;","<").Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>","").Replace("<string xmlns=\"http://tempuri.org/\">","");
                _loadDataComplete = true;
                MessageBox.Show(responseFromServer);
                reader.Close();
                stream.Close();
                res.Close();
                IncrementProgressBar(70);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IncrementProgressBar(int i)
        {
            toolStripProgressBar2.Increment(i);
        }
    }
}
