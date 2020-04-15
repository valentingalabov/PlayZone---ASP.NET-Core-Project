namespace PlayZone.Data.Models
{
    using PlayZone.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string VideoId { get; set; }

        public virtual Video Video { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
