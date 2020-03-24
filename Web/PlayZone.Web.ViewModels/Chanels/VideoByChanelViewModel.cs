namespace PlayZone.Web.ViewModels.Chanels
{
    using System;

    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class VideoByChanelViewModel : IMapFrom<Video>
    {
        public string Id { get; set; }

        public string UserUserName { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AddBefore
        {
            get
            {
                int days = (DateTime.UtcNow - this.CreatedOn).Days;

                int month = days / 30;

                int year = days / 365;
                if (year > 0)
                {
                    if (year == 1)
                    {
                        return year.ToString() + " Year";
                    }
                    else
                    {
                        return year.ToString() + " Years";
                    }
                }
                else if (month > 0)
                {
                    if (month == 1)
                    {
                        return month.ToString() + " Month";
                    }
                    else
                    {
                        return month.ToString() + " Months";
                    }
                }
                else if (days >= 1 && days <= 30)
                {
                    if (days == 1)
                    {
                        return days.ToString() + " Day";
                    }
                    else
                    {
                        return days.ToString() + " Days";
                    }
                }
                else
                {
                    int hours = (DateTime.UtcNow - this.CreatedOn).Hours;

                    if (hours == 1)
                    {
                        return hours.ToString() + " Hour";
                    }
                    else
                    {
                        return hours.ToString() + " Hours";
                    }
                }
            }
        }
    }
}
