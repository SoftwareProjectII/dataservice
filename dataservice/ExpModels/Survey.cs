using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
{
    public partial class Survey
    {
        public Survey()
        {
            Surveyquestion = new HashSet<Surveyquestion>();
            Traininginfo = new HashSet<Traininginfo>();
            Trainingsession = new HashSet<Trainingsession>();
            Trainingsurvey = new HashSet<Trainingsurvey>();
        }

        public int SurveyId { get; set; }

        public ICollection<Surveyquestion> Surveyquestion { get; set; }
        public ICollection<Traininginfo> Traininginfo { get; set; }
        public ICollection<Trainingsession> Trainingsession { get; set; }
        public ICollection<Trainingsurvey> Trainingsurvey { get; set; }
    }
}
