using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class User
    {
        public User()
        {
            Followingtraining = new HashSet<Followingtraining>();
            Surveyanswer = new HashSet<Surveyanswer>();
            Usercertificate = new HashSet<Usercertificate>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? EmpId { get; set; }
        public byte[] Salt { get; set; }
    

        [JsonIgnore]
        public ICollection<Followingtraining> Followingtraining { get; set; }
        [JsonIgnore]
        public ICollection<Surveyanswer> Surveyanswer { get; set; }
        [JsonIgnore]
        public ICollection<Usercertificate> Usercertificate { get; set; }
    }
}
