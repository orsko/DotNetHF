using System;

namespace DotNetHF.Questions
{
    public partial class InsertQuestionNotification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string question = Request.QueryString["Question"];
            string date = Request.QueryString["Date"];

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Questions/List.aspx?NeedUpload=true");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Questions/List.aspx");
        }
    }
}
