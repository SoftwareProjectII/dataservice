using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
{
    public partial class Usercertificate
    {
        public int UserId { get; set; }
        public int TrainingId { get; set; }
        public int CertificateId { get; set; }

        public Certificate Certificate { get; set; }
        public Traininginfo Training { get; set; }
        public User User { get; set; }
    }
}
