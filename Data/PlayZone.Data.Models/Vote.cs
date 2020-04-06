namespace PlayZone.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PlayZone.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public string VideoId { get; set; }

        public virtual Video Video { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public VoteType Type { get; set; }
    }
}
