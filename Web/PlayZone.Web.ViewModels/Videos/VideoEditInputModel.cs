namespace PlayZone.Web.ViewModels.Videos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class VideoEditInputModel : IMapFrom<Video>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Url is Required!")]
        [RegularExpression(@"^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$", ErrorMessage = "Invalid Url!")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [MinLength(4, ErrorMessage = "Length cannot be shorter than 4 characters!")]
        [MaxLength(60, ErrorMessage = "Length cannot be longer than 60 characters!")]
        public string Title { get; set; }

        [MaxLength(6000, ErrorMessage = "Length cannot be longer than 6000 characters!")]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Categories")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public string ExistingTitle { get; set; }

        public string ExistingUrl { get; set; }
    }
}
