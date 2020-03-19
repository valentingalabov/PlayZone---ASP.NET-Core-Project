namespace PlayZone.Web.ViewModels.Chanels
{
    using System.ComponentModel.DataAnnotations;

    using PlayZone.Common.ModelValidation;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class ChanelCreateInputModel 
    {
        [Required(ErrorMessage = "Title is Required!")]
        [MaxLength(VideosAndChanelsModelValidation.MaxLenght, ErrorMessage = VideosAndChanelsModelValidation.TitleLenghtErrorMessage)]
        [MinLength(VideosAndChanelsModelValidation.MinLenght, ErrorMessage = VideosAndChanelsModelValidation.TitleLenghtErrorMessage)]
        public string Title { get; set; }

        [MaxLength(VideosAndChanelsModelValidation.DescriptionMaxLenght, ErrorMessage = VideosAndChanelsModelValidation.DescriptionErrorMessage)]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
