using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdf
{
    public class data
    {
        public List<data> Questions()
        {
            var questions = new List<data>();

            questions.Add(new data { QuestionID = "1", Message = @"<span><b>bold</b> and <em>hardsome</em> plus image</span><img src='win_20150616_141504.jpg' style='width:320px; height:240px; float:right;' />" });
            questions.Add(new data { QuestionID = "2", Message = "<span><b>bold</b> and <em>hardsome</em> plus image</span>" });
            questions.Add(new data { QuestionID = "3", Message = "<span><b>bold</b> and <em>hardsome</em> plus image</span>" });
            questions.Add(new data { QuestionID = "4", Message = "<span><b>bold</b> and <em>hardsome</em> plus image</span>" });
            questions.Add(new data { QuestionID = "5", Message = "<span><b>bold</b> and <em>hardsome</em> plus image</span>" });
            questions.Add(new data { QuestionID = "6", Message = "<span><b>bold</b> and <em>hardsome</em> plus image</span>" });
            questions.Add(new data { QuestionID = "7", Message = "<span><b>bold</b> and <em>hardsome</em> plus image</span>" });
            questions.Add(new data { QuestionID = "8", Message = "<span><b>bold</b> and <em>hardsome</em> plus image</span>" });
            
            return questions;
        }

        public string QuestionID { get; set; }

        public string Message { get; set; }
    }
}
