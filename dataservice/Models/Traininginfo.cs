namespace dataservice.Models
{
    public partial class Traininginfo
    {
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string InfoGeneral { get; set; }
        public int NumberOfDays { get; set; }
        public string InfoExam { get; set; }
        public string InfoPayment { get; set; }
        public double Price { get; set; }
    }
}
