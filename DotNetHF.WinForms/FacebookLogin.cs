using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;

namespace DotNetHF.WinForms
{
    public partial class FacebookLogin : Form
    {
        private static string _accessToken;

        public static string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        private const string applicationId = "339276276175617";
        private const string applicationSecret = "b53977b8c4d5af6e310dcbd3af2c69f4";
        private Form form;

        public FacebookLogin(Form main)
        {
            form = main;
            InitializeComponent();
            Login();
        }

        private void Login()
        {
            var client = new FacebookClient { AppId = applicationId };
            var parameters = new Dictionary<string, object>
            {
                { "response_type", "token" },
                { "redirect_uri", "https://www.facebook.com/connect/login_success.html"},
                { "scope", "publish_stream"},
            };
            var loginUri = client.GetLoginUrl(parameters);
            webBrowser1.Navigate(loginUri);
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            FacebookOAuthResult result;
            var client = new FacebookClient();
            if (client.TryParseOAuthCallbackUrl(e.Url, out result))
            {
                // token eltarolasa
                _accessToken = result.AccessToken;
            }
            else
            {
                form.Show();
                Close();
            }
        }
    }
}
