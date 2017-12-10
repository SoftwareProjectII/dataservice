using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dataservice.Models
{
    public class FollowingtrainingUpdate
    {
        public FollowingtrainingUpdate()
        {

        }
        public FollowingtrainingUpdate(Followingtraining f)
        {
            UserId = f.UserId;
            TrainingSessionId = f.TrainingSessionId;
            IsApproved = f.IsApproved;
            IsCancelled = f.IsCancelled;
        }

        public int UserId { get; set; }
        public int TrainingSessionId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCancelled { get; set; }
    }
}
