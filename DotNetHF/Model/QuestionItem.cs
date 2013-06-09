using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetHF.Model
{
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
}