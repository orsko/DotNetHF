using System;

namespace DotNetHF.Questions
{
    public partial class InsertQuestionNotification : System.Web.UI.Page
    {
        public string Question { get; set; }
        public string Date { get; set; }    
        protected void Page_Load(object sender, EventArgs e)
        {
            Question = Request.QueryString["Question"];
            Date = Request.QueryString["Date"].Split(' ')[0];
            Page.DataBind();
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
