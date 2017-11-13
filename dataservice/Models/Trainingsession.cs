using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Trainingsession
    {
        public Trainingsession()
        {
            Followingtraining = new HashSet<Followingtraining>();
        }

        public int TrainingSessionId { get; set; }
        public int AddressId { get; set; }
        public int TeacherId { get; set; }
        public int TrainingId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public bool Cancelled { get; set; }

        public Address Address { get; set; }
        public Teacher Teacher { get; set; }
        public Traininginfo Training { get; set; }
        public ICollection<Followingtraining> Followingtraining { get; set; }
    }
}
