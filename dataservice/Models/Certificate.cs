﻿using Newtonsoft.Json;

namespace dataservice.Models
{
    public partial class Certificate
    {
        public int CertificateId { get; set; }
        public int TrainingId { get; set; }
        public string Titel { get; set; }
        public byte[] Picture { get; set; }

        [JsonIgnore]
        public Traininginfo Training { get; set; }
    }
}
