using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                Task.Factory.StartNew(LoadDataBase);
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
            toolStripProgressBar1.Increment(50);
            toolStripProgressBar1.Dispose();
        }

        private void LoadDataBase()
        {
            
        }

        private void IncrementProgressBar(int i)
        {
            toolStripProgressBar2.Increment(i);
        }
    }
}
