using Newtonsoft.Json;

namespace dataservice.Models
{
    public partial class Trainingfaq
    {
        public int TrainingId { get; set; }
        public int FaqId { get; set; }

        [JsonIgnore]
        public Faq Faq { get; set; }
        [JsonIgnore]
        public Traininginfo Training { get; set; }
    }
}
