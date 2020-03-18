namespace PlayZone.Web.ViewModels.Videos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class VideoCreateInputModel : IMapFrom<Video>
    {
        [Required(ErrorMessage = "Url is Required!")]
        [MinLength(3, ErrorMessage = "Url must be in range between 3 and 60 symbols!")]
        [MaxLength(60, ErrorMessage = "Url must be in range between 3 and 60 symbols!")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(60, ErrorMessage = "Title must be in range between 4 and 60 symbols!")]
        [MinLength(4, ErrorMessage = "Title must be in range between 4 and 60 symbols!")]
        public string Title { get; set; }

        [MaxLength(6000, ErrorMessage = "Description can't be more than 6000 symbols!")]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Categories")]

        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
