using Newtonsoft.Json;

namespace dataservice.Models
{
    public partial class Infotosession
    {
        public int TrainingId { get; set; }
        public int TrainingSessionId { get; set; }

        [JsonIgnore]
        public Traininginfo Training { get; set; }
    }
}
