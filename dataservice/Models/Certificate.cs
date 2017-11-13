using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Certificate
    {
        public int CertificateId { get; set; }
        public int TrainingId { get; set; }
        public string Titel { get; set; }
        public byte[] Picture { get; set; }

        public Traininginfo Training { get; set; }
    }
}
