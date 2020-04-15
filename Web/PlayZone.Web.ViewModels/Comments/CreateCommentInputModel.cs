namespace PlayZone.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public string VideoId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
