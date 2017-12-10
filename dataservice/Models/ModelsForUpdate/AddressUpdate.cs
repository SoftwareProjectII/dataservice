using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dataservice.Models
{
    public class AddressUpdate
    {
        public AddressUpdate()
        {

        }
        public AddressUpdate(Address a)
        {
            AddressId = a.AddressId;
            AdministrativeArea = a.AdministrativeArea;
            Locality = a.Locality;
            PostalCode = a.PostalCode;
            StreetAddress = a.StreetAddress;
            Premise = a.Premise;
            Country = a.Country;
        }

        public int AddressId { get; set; }
        public string AdministrativeArea { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public string Premise { get; set; }
        public string Country { get; set; }
    }
}
