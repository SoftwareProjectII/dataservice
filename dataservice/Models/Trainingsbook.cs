using Newtonsoft.Json;

namespace dataservice.Models
{
    public partial class Trainingsbook
    {
        public int TrainingId { get; set; }
        public int Isbn { get; set; }
        
        public Book IsbnNavigation { get; set; }
        public Traininginfo Training { get; set; }
    }
}
