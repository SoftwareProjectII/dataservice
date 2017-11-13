using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Followingtraining
    {
        public int UserId { get; set; }
        public int TrainingSessionId { get; set; }

        public Trainingsession TrainingSession { get; set; }
        public User User { get; set; }
    }
}
