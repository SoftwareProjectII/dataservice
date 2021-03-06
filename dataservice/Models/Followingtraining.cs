﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Followingtraining
    {
        public int UserId { get; set; }
        public int TrainingSessionId { get; set; }

        [JsonIgnore]
        public Trainingsession TrainingSession { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
