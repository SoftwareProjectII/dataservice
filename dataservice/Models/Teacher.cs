using Newtonsoft.Json;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Trainingsession = new HashSet<Trainingsession>();
        }

        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        public ICollection<Trainingsession> Trainingsession { get; set; }
    }
}
