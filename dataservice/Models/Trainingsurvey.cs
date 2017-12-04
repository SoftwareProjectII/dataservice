using Newtonsoft.Json;

namespace dataservice.Models
{
    public partial class Trainingsurvey
    {
        public int TrainingId { get; set; }
        public int SurveyId { get; set; }
        
        public Survey Survey { get; set; }
        public Traininginfo Training { get; set; }
    }
}
