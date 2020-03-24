namespace PlayZone.Web.ViewModels.Chanels
{
    using System.Collections.Generic;

    public class AllVideosByChanelViewModel
    {
        public IEnumerable<VideoByChanelViewModel> Videos { get; set; }

        public ChanelViewModel Chanel { get; set; }
    }
}
