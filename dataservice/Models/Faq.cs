using Newtonsoft.Json;
using System.Collections.Generic;

namespace dataservice.Models
{
    public partial class Faq
    {
        public Faq()
        {
            Trainingfaq = new HashSet<Trainingfaq>();
        }

        public int FaqId { get; set; }
        public string QuestionFaq { get; set; }
        public string AnswerFaq { get; set; }
        [JsonIgnore]
        public ICollection<Trainingfaq> Trainingfaq { get; set; }
    }
}
