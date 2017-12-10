using Newtonsoft.Json;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Survey
    {
        public Survey()
        {
            Surveyquestion = new HashSet<Surveyquestion>();
            Traininginfo = new HashSet<Traininginfo>();
            Trainingsession = new HashSet<Trainingsession>();
            Trainingsurvey = new HashSet<Trainingsurvey>();
        }

        public int SurveyId { get; set; }
        public ICollection<Surveyquestion> Surveyquestion { get; set; }
        [JsonIgnore]
        public ICollection<Traininginfo> Traininginfo { get; set; }
        [JsonIgnore]
        public ICollection<Trainingsession> Trainingsession { get; set; }
        [JsonIgnore]
        public ICollection<Trainingsurvey> Trainingsurvey { get; set; }
    }
}
