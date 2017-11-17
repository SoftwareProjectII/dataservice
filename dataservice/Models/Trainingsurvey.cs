using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Trainingsurvey
    {
        public int TrainingId { get; set; }
        public int SurveyId { get; set; }

        [JsonIgnore]
        public Survey Survey { get; set; }
        [JsonIgnore]
        public Traininginfo Training { get; set; }
    }
}
