using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
{
    public partial class Followingtraining
    {
        public int UserId { get; set; }
        public int TrainingSessionId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsDeclined { get; set; }

        public Trainingsession TrainingSession { get; set; }
        public User User { get; set; }
    }
}
