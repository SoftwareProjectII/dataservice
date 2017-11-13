using Newtonsoft.Json;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Address
    {
        public Address()
        {
            Trainingsession = new HashSet<Trainingsession>();
        }

        public int AddressId { get; set; }
        public string AdministrativeArea { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public string Premise { get; set; }
        public string Country { get; set; }


        [JsonIgnore]
        public ICollection<Trainingsession> Trainingsession { get; set; }
    }
}
