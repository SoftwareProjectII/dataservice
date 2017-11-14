using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Surveyquestion
    {
        public Surveyquestion()
        {
            Surveyanswer = new HashSet<Surveyanswer>();
        }

        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string Content { get; set; }

        [JsonIgnore]
        public Survey Survey { get; set; }
        [JsonIgnore]
        public ICollection<Surveyanswer> Surveyanswer { get; set; }
    }
}
