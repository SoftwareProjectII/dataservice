using Newtonsoft.Json;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Book
    {
        public Book()
        {
            Trainingsbook = new HashSet<Trainingsbook>();
        }

        public long Isbn { get; set; }
        public string Url { get; set; }

        public ICollection<Trainingsbook> Trainingsbook { get; set; }
    }
}
