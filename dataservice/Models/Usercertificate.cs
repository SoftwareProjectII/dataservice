using Newtonsoft.Json;

namespace dataservice.Models
{
    public partial class Usercertificate
    {
        public int UserId { get; set; }
        public int TrainingId { get; set; }
        public int CertificateId { get; set; }
        [JsonIgnore]
        public Certificate Certificate { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Traininginfo Training { get; set; }
    }
}
