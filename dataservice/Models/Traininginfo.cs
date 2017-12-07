using Newtonsoft.Json;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Traininginfo
    {
        public Traininginfo()
        {
            Certificate = new HashSet<Certificate>();
            Trainingfaq = new HashSet<Trainingfaq>();
            Trainingsbook = new HashSet<Trainingsbook>();
            Trainingsession = new HashSet<Trainingsession>();
            Trainingsurvey = new HashSet<Trainingsurvey>();
        }

        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string InfoGeneral { get; set; }
        public int NumberOfDays { get; set; }
        public string InfoExam { get; set; }
        public string InfoPayment { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public ICollection<Certificate> Certificate { get; set; }
        [JsonIgnore]
        public ICollection<Trainingfaq> Trainingfaq { get; set; }
        [JsonIgnore]
        public ICollection<Trainingsbook> Trainingsbook { get; set; }
        [JsonIgnore]
        public ICollection<Trainingsession> Trainingsession { get; set; }
        [JsonIgnore]
        public ICollection<Trainingsurvey> Trainingsurvey { get; set; }
    }
}
