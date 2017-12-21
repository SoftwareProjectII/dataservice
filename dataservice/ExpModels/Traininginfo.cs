using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
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
            Usercertificate = new HashSet<Usercertificate>();
        }

        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string InfoGeneral { get; set; }
        public int NumberOfDays { get; set; }
        public string InfoExam { get; set; }
        public string InfoPayment { get; set; }
        public double Price { get; set; }
        public int? SurveyId { get; set; }

        public Survey Survey { get; set; }
        public ICollection<Certificate> Certificate { get; set; }
        public ICollection<Trainingfaq> Trainingfaq { get; set; }
        public ICollection<Trainingsbook> Trainingsbook { get; set; }
        public ICollection<Trainingsession> Trainingsession { get; set; }
        public ICollection<Trainingsurvey> Trainingsurvey { get; set; }
        public ICollection<Usercertificate> Usercertificate { get; set; }
    }
}
