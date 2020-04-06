namespace PlayZone.Data.Models
{
    using PlayZone.Data.Common.Models;

    public class VideoHistory : BaseDeletableModel<int>
    {
        public string VideoId { get; set; }

        public virtual Video Video { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
