using Newtonsoft.Json;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Survey
    {
        public Survey()
        {
            Surveyquestion = new HashSet<Surveyquestion>();
            Trainingsurvey = new HashSet<Trainingsurvey>();
        }

        public int SurveyId { get; set; }
        [JsonIgnore]
        public ICollection<Surveyquestion> Surveyquestion { get; set; }
        [JsonIgnore]
        public ICollection<Trainingsurvey> Trainingsurvey { get; set; }
    }
}
