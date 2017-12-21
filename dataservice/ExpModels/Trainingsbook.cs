using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
{
    public partial class Trainingsbook
    {
        public int TrainingId { get; set; }
        public long Isbn { get; set; }

        public Book IsbnNavigation { get; set; }
        public Traininginfo Training { get; set; }
    }
}
