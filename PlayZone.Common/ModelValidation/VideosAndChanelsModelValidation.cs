namespace PlayZone.Common.ModelValidation
{
    public static class VideosAndChanelsModelValidation
    {
        public const int DescriptionMaxLenght = 6000;

        public const string DescriptionErrorMessage = "Description can't be more than 6000 symbols!";

        public const int MaxLenght = 60;

        public const int MinLenght = 4;

        public const string TitleLenghtErrorMessage = "Title must be in range between 4 and 60 symbols!";

        public const string UrlLenghtErrorMessage = "Url must be in range between 4 and 60 symbols!";

    }
}
