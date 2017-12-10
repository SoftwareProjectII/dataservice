using Newtonsoft.Json;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Certificate
    {
        public Certificate()
        {
            Usercertificate = new HashSet<Usercertificate>();
        }

        public int CertificateId { get; set; }
        public int TrainingId { get; set; }
        public string Titel { get; set; }
        public byte[] Picture { get; set; }

        [JsonIgnore]
        public Traininginfo Training { get; set; }
        [JsonIgnore]
        public ICollection<Usercertificate> Usercertificate { get; set; }
    }
}
