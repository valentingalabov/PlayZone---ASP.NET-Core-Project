namespace PlayZone.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlayZone.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Videos = new HashSet<Video>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<Video> Videos { get; set; }
    }
}
