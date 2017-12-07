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

        public Address(AddressUpdate u)
        {
            AddressId = u.AddressId;
            AdministrativeArea = u.AdministrativeArea;
            Locality = u.Locality;
            PostalCode = u.PostalCode;
            StreetAddress = u.StreetAddress;
            Premise = u.Premise;
            Country = u.Country;
        }

        public int AddressId { get; set; }
        public string AdministrativeArea { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public string Premise { get; set; }
        public string Country { get; set; }
        public ICollection<Trainingsession> Trainingsession { get; set; }
    }
}
