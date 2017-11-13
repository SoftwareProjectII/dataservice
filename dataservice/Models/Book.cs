using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Book
    {
        public int Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string Publisher { get; set; }
    }
}
