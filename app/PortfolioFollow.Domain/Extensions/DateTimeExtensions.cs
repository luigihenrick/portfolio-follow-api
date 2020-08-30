using Nager.Date;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Service.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetLastWorkDay(this DateTime dateToSearch)
        {
            if (dateToSearch.DayOfWeek == DayOfWeek.Saturday || dateToSearch.DayOfWeek == DayOfWeek.Sunday)
            {
                return GetLastWorkDay(dateToSearch.AddDays(-1));
            }

            if (DateSystem.IsPublicHoliday(dateToSearch, CountryCode.BR))
            {
                return GetLastWorkDay(dateToSearch.AddDays(-1));
            }

            return dateToSearch;
        }
    }
}
