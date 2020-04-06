namespace PlayZone.Web.ViewModels.Videos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlayZone.Common.ModelValidation;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class VideoCreateInputModel : IMapFrom<Video>
    {
        [Required(ErrorMessage = "Url is Required!")]
        [RegularExpression(@"^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$", ErrorMessage = "Invalid Url!")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [MinLength(VideosAndChanelsModelValidation.MinLenght, ErrorMessage = VideosAndChanelsModelValidation.TitleLenghtErrorMessage)]
        [MaxLength(VideosAndChanelsModelValidation.MaxLenght, ErrorMessage = VideosAndChanelsModelValidation.TitleLenghtErrorMessage)]
        public string Title { get; set; }

        [MaxLength(VideosAndChanelsModelValidation.DescriptionMaxLenght, ErrorMessage = VideosAndChanelsModelValidation.DescriptionErrorMessage)]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Categories")]

        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
