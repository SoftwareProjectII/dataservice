using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
{
    public partial class Trainingsurvey
    {
        public int TrainingId { get; set; }
        public int SurveyId { get; set; }

        public Survey Survey { get; set; }
        public Traininginfo Training { get; set; }
    }
}
