namespace PlayZone.Data.Models
{
    using PlayZone.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Body { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
