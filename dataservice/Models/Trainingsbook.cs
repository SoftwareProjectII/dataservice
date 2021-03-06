﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Trainingsbook
    {
        public int TrainingId { get; set; }
        public int Isbn { get; set; }

        [JsonIgnore]
        public Book IsbnNavigation { get; set; }
        [JsonIgnore]
        public Traininginfo Training { get; set; }
    }
}
