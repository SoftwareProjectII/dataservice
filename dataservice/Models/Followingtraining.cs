using Newtonsoft.Json;

namespace dataservice.Models
{
    public partial class Followingtraining
    {
        public int UserId { get; set; }
        public int TrainingSessionId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCancelled { get; set; }

        [JsonIgnore]
        public Trainingsession TrainingSession { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
