/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace I_vigil.BO
{
    public class CalendarEventTime
    {
        DateTime _eventTime = new DateTime();

        public DateTime EventTime
        {
            get { return _eventTime; }
            set { _eventTime = value; }
        }

        string type = "";

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        string siteName = "";

        public string SiteName
        {
            get { return siteName; }
            set { siteName = value; }
        }

        CalendarEvents siteCalendarEvent = new CalendarEvents();

        public CalendarEvents SiteCalendarEvent
        {
            get { return siteCalendarEvent; }
            set { siteCalendarEvent = value; }
        }

        bool isDisplayed = false;

        public bool IsDisplayed
        {
            get { return isDisplayed; }
            set { isDisplayed = value; }
        }
    }
}
