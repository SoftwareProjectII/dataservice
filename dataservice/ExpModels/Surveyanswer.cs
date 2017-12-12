using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
{
    public partial class Surveyanswer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int? UserId { get; set; }

        public Surveyquestion Question { get; set; }
        public User User { get; set; }
    }
}
