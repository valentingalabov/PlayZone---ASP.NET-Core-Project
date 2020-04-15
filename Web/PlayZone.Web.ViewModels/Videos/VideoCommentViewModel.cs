namespace PlayZone.Web.ViewModels.Videos
{
    using System;

    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class VideoCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }
    }
}
