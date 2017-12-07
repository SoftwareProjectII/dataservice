namespace dataservice.Models
{
    public partial class Followingtraining
    {
        public Followingtraining()
        {

        }

        public Followingtraining(FollowingtrainingUpdate u)
        {
            UserId = u.UserId;
            TrainingSessionId = u.TrainingSessionId;
            IsApproved = u.IsApproved;
            IsCancelled = u.IsCancelled;
        }

        public int UserId { get; set; }
        public int TrainingSessionId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCancelled { get; set; }
        
        public Trainingsession TrainingSession { get; set; }        
        public User User { get; set; }
    }
}
