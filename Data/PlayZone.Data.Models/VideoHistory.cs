namespace PlayZone.Data.Models
{
    using PlayZone.Data.Common.Models;

    public class VideoHistory : BaseDeletableModel<int>
    {
        public string VideoId { get; set; }

        public Video Video { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

    }
}
