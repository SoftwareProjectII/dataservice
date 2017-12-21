using System;
using System.Collections.Generic;

namespace dataservice.ExpModels
{
    public partial class Trainingfaq
    {
        public int TrainingId { get; set; }
        public int FaqId { get; set; }

        public Faq Faq { get; set; }
        public Traininginfo Training { get; set; }
    }
}
