using Newtonsoft.Json;

namespace dataservice.Models
{
    public partial class Usercertificate
    {
        public int UserId { get; set; }
        public int CertificateId { get; set; }
        
        public Certificate Certificate { get; set; }
        public User User { get; set; }
    }
}
