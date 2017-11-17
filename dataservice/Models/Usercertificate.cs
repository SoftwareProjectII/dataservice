using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Usercertificate
    {
        public int UserId { get; set; }
        public int CertificateId { get; set; }

        [JsonIgnore]
        public Certificate Certificate { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
