/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Diagnostics;
using I_vigil.Util;

namespace I_vigil.BO
{
    /*
     * Calendar Events Class
     * 
     * */
    public class CalendarEvents
    {
        //calendar events object
        private ProvigilService.CalendarEvent _proxyCalendarEvents = null;
        //calendar start time
        private string _startTime = "";
        //calendar end time
        private string _endTime = "";
        //calendar Start Date
        private string _startDate = "";
        //calendar End Date
        private string _endDate = "";
        //calendar event sitename
        private string _siteName = "";

        private bool _holidayMonitoring = false;

        public bool HolidayMonitoring
        {
            get { return _holidayMonitoring; }
            set { _holidayMonitoring = value; }
        }
        
        public string SiteName
        {
            get { return _siteName; }
            set { _siteName = value; }
        }
        /// <summary>
        /// gets or Sets startTime
        /// </summary>
        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        
        /// <summary>
        /// gets and sets EndTime
        /// </summary>
        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        
        /// <summary>
        /// gets and sets StartDate
        /// </summary>
        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        
        /// <summary>
        /// gets and sets End Date
        /// </summary>
        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        
        /// <summary>
        /// blank constructor
        /// </summary>
        public CalendarEvents()
        { 
        }

        /// <summary>
        /// Constructor with calendar Events service object
        /// </summary>
        /// <param name="calendarEvents"></param>
        public CalendarEvents(ProvigilService.CalendarEvent calendarEvents)
        {
            _proxyCalendarEvents = calendarEvents;
            InitDateValues();
        }

        /// <summary>
        /// Initialise Date Values
        /// </summary>
        private void InitDateValues()
        {
            try
            {
                //Initialising the start Date 
                _startDate = _proxyCalendarEvents.start_Date;

                //setting the end date value
                _endDate = _proxyCalendarEvents.end_Date;

                //setting the start time value
                _startTime = _proxyCalendarEvents.startTime;

                _endTime = _proxyCalendarEvents.endTime;

                _siteName = _proxyCalendarEvents.siteName;

                _holidayMonitoring = _proxyCalendarEvents.holidayMonitoring;
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Exception CalendarEvent.IntiDateValues "+ex.Message);
            }
        }

        /// <summary>
        /// Gets calendar events Subject
        /// </summary>
        public string Subject 
        {
            get { return _proxyCalendarEvents.subject; }
        }

        /// <summary>
        /// Get the calendar events status whether enabled or not
        /// </summary>
        public bool isDisabled
        {
            get { return _proxyCalendarEvents.disabled; }
        }

        /// <summary>
        /// get the calendar events Status
        /// </summary>
        public string EventStatus
        {
            get { return _proxyCalendarEvents.eventStatus; }
        }

        /// <summary>
        /// get the site server Time
        /// </summary>
        public string ServerTime
        {
            get { return _proxyCalendarEvents.serverTime; }
        }

    }
}
