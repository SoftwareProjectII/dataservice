﻿using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
{
    public partial class Surveyquestion
    {
        public Surveyquestion()
        {
            Surveyanswer = new HashSet<Surveyanswer>();
        }

        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string Content { get; set; }

        public Survey Survey { get; set; }
        public ICollection<Surveyanswer> Surveyanswer { get; set; }
    }
}