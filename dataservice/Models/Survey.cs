using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Survey
    {
        public Survey()
        {
            Surveyquestion = new HashSet<Surveyquestion>();
        }

        public int SurveyId { get; set; }

        public ICollection<Surveyquestion> Surveyquestion { get; set; }
    }
}
