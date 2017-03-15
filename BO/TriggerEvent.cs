/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Diagnostics;
using I_vigil.Util;

namespace I_vigil.BO
{
    /*
     * Trigger Event Class 
     */
    public class TriggerEvent
    {
        //Trigger Event Object
        private ProvigilService.TriggerEvent _proxyTriggerEvent;
        //Output Time
        private string _outputTime = "";
        /// <summary>
        /// Gets and Sets output time
        /// </summary>
        public string OutputTime
        {
            get { return _outputTime; }
            set { _outputTime = value; }
        }

        /// <summary>
        /// Trigger Event Id
        /// </summary>
        public long TriggerEventId
        {
            get { return _proxyTriggerEvent.id; }
        }

        /// <summary>
        /// Blank Constructor
        /// </summary>
        public TriggerEvent()
        { 
        }

        /// <summary>
        /// Constructor with TriggerEvent Object
        /// </summary>
        /// <param name="triggerEvent"></param>
        public TriggerEvent(ProvigilService.TriggerEvent triggerEvent)
        {
            _proxyTriggerEvent = triggerEvent;
            InitOutputTime();
        }

        /// <summary>
        /// Initialise output Time
        /// </summary>
        private void InitOutputTime()
        {
            try
            {
                //Trigger Object timestamp
                DateTime time = _proxyTriggerEvent.timeStamp.Value;
                //Initialise the serverTime Zone
                TimeZoneInfo serverTimeZone = (TimeZoneInfo)App.Current.Properties["ServerTimeZone"];
                //Check if the server time zone is null
                if (serverTimeZone != null)
                    //Converts the time zone to Universal time
                    time = TimeZoneInfo.ConvertTimeFromUtc(_proxyTriggerEvent.timeStamp.Value, serverTimeZone);
                //Sets the output time
                _outputTime = time.ToString("HH:mm:ss");
            }
            catch (Exception ex) { Logger.LogDebug(ex.StackTrace); }
        }


        /// <summary>
        /// Gets the Description
        /// </summary>
        public string Description
        {
            get { return _proxyTriggerEvent.description; }
        }
        /// <summary>
        /// Gets the Status
        /// </summary>
        public string Status
        {
            get { return (_proxyTriggerEvent.status)?"Success":"Failed"; }
        }
        /// <summary>
        /// Gets the Typw
        /// </summary>
        public string Type
        {
            get { return _proxyTriggerEvent.type; }
        }
        /// <summary>
        /// Gets the UserName
        /// </summary>
        public string Username 
        {
            get { return _proxyTriggerEvent.userName; }
        }
        /// <summary>
        /// Gets the Output Image
        /// </summary>
        public string OutputImage
        {
            get
            {
                //strobe Image 
                string uri = "images\\strobe-on.png";
                //Check if Decription is Trigger ON set strobe on image
                if(Description == "Trigger On" )
                    uri = (Type == "STROBE") ? "images\\strobe-on.png" : "images\\siren-on.png";
                //Check if Decription is Trigger ON set strobe off Image
                else if (Description == "Trigger Off" )
                    uri = (Type == "STROBE") ? "images\\strobe-off.png" : "images\\siren-off.png";
                //Check if Decription is Trigger ON Audio Image
                else if (Type == "AUDIO")
                    uri = "images\\audio.png";
                //Return the URI
                return uri;
            }
        }

    }
}
