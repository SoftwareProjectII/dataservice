using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Surveyanswer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int? UserId { get; set; }

        [JsonIgnore]
        public Surveyquestion Question { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
