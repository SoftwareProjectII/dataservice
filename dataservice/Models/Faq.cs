using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Faq
    {
        public int FaqId { get; set; }
        public string QuestionFaq { get; set; }
        public string AnswerFaq { get; set; }
    }
}
