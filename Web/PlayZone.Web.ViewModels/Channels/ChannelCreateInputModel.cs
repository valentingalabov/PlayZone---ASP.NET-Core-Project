namespace PlayZone.Web.ViewModels.Channels
{
    using System.ComponentModel.DataAnnotations;

    public class ChannelCreateInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is Required!")]
        [MaxLength(30, ErrorMessage = "Length cannot be longer than 30 characters!")]
        [MinLength(4, ErrorMessage = "Length cannot be shorter than 4 characters!")]
        public string Title { get; set; }

        [MaxLength(6000, ErrorMessage = "Description can't be more than 6000 characters!)")]
        public string Description { get; set; }

        public string UserId { get; set; }

        public string ExistTitle { get; set; }
    }
}
