namespace PlayZone.Data.Models
{
    using System;

    using PlayZone.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Url { get; set; }

        public string CloudinaryPublicId { get; set; }

        public string ChanelId { get; set; }

        public virtual Chanel Chanel { get; set; }
    }
}
