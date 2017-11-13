using Newtonsoft.Json;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class User
    {
        public User()
        {
            Followingtraining = new HashSet<Followingtraining>();
            Surveyanswer = new HashSet<Surveyanswer>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public ICollection<Followingtraining> Followingtraining { get; set; }
        [JsonIgnore]
        public ICollection<Surveyanswer> Surveyanswer { get; set; }
    }
}
