using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Traininginfo
    {
        public Traininginfo()
        {
            Certificate = new HashSet<Certificate>();
            Infotosession = new HashSet<Infotosession>();
            Trainingsession = new HashSet<Trainingsession>();
        }

        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string InfoGeneral { get; set; }
        public int NumberOfDays { get; set; }
        public string InfoExam { get; set; }
        public string InfoPayment { get; set; }
        public double Price { get; set; }

        public ICollection<Certificate> Certificate { get; set; }
        public ICollection<Infotosession> Infotosession { get; set; }
        public ICollection<Trainingsession> Trainingsession { get; set; }
    }
}
