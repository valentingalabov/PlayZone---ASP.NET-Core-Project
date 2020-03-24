namespace PlayZone.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlayZone.Data.Common.Models;

    public class Video : BaseDeletableModel<string>
    {
        public Video()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string UserId { get; set; }

        [Required]
        public string ChanelId { get; set; }

        public virtual Chanel Chanel { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
