using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class TrainingsessionUpdate
    {
        public TrainingsessionUpdate()
        {
            
        }

        public TrainingsessionUpdate(Trainingsession ts)
        {
            TrainingSessionId = ts.TrainingSessionId;
            AddressId = ts.AddressId;
            TeacherId = ts.TeacherId;
            TrainingId = ts.TrainingId;
            Date = ts.Date;
            StartHour = ts.StartHour;
            EndHour = ts.EndHour;
            Cancelled = ts.Cancelled;
            SurveyId = ts.SurveyId;
        }

        public int TrainingSessionId { get; set; }
        public int AddressId { get; set; }
        public int TeacherId { get; set; }
        public int TrainingId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public bool Cancelled { get; set; }
        public int? SurveyId { get; set; }
    }
}
