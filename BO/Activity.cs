/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Diagnostics;
using I_vigil.Util;

namespace I_vigil.BO
{
    /**
      * Activity Class
     */
    public class Activity
    {
        //VISITED Activity
        public static int VISITED = 1;
        //FALSE Activity
        public static int FALSED = 2;

        //camera Id
        private string _cameraId = null;
        //time stamp 
        private DateTime _timeStamp;
        //visited Tag
        private bool _visited = false;
        //Falsed tag
        private bool _falsed = false;
        private string _activityType = null;
        private string _description = null;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string ActivityType
        {
            get { return _activityType; }
            set { _activityType = value; }
        }

        // duration in minutes
        private int _duration;

        private bool markedInLive = false;

        public bool MarkedInLive
        {
            get { return markedInLive; }
            set { markedInLive = value; }
        }
        /// <summary>
        /// Gets or Sets CameraId 
        /// </summary>
        public string CameraId
        {
            get { return _cameraId; }
            set { _cameraId = value; }
        }
        /// <summary>
        /// Gets or Sets TimeStamp
        /// </summary>
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }
        /// <summary>
        /// Gets Timestamp
        /// </summary>
        public string Time
        {
            get { return TimeStamp.TimeOfDay.ToString(); }
        }

        /// <summary>
        /// Gets or Sets visited tag
        /// </summary>
        public bool Visited
        {
            get { return _visited; }
            set { _visited = value; }
        }
        
        /// <summary>
        /// Gets and Sets falsed tag
        /// </summary>
        public bool Falsed
        {
            get { return _falsed; }
            set { _falsed = value; }
        }
        
        /// <summary>
        /// Gets and Sets Duration
        /// </summary>
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        private int _activityTag;

        public int ActivityTag
        {
            get { return _activityTag; }
            set { _activityTag = value; }
        }

        /// <summary>
        /// Blank Constructor 
        /// </summary>
        public Activity()
        { 
        }

        public Activity(string values)
        {
            char[] seperator = { ',' };
            string[] value = values.Split(seperator);

            //Converting the value to datetime
            InitDateValue(value[1]);

            //Initialising the cameraId
            this.CameraId = value[0];
            //Intialising Visited Flag
            this.Visited = GetBoolValue(value[2]);
            //Intialising Falsed Flag
            this.Falsed = GetBoolValue(value[3]);
            //Initialiseing the activity type whether bookmark or activity
            this.ActivityType = value[5];
            this.Description = value[6];
            try
            {
                //get the duration
                int duration = Int32.Parse(value[4]);
                //Initialising Duration to the activity
                this.Duration = (duration / 60 == 0) ? 1 : duration / 60;
            }
            catch (Exception ex)
            {
                //If no duration, initialise to 1
                Logger.LogDebug(ex.StackTrace);
                this.Duration = 1;
            }
            try
            {
                if (value.Length > 7)
                    this.MarkedInLive = Convert.ToBoolean(value[7]);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Activity(string values) " + ex.Message);
            }
            if (value.Length > 8)
            {
                this.ActivityTag = int.Parse(value[8]);
                Logger.LogDebug(String.Format("{0} - {1} - tag = {2}", CameraId, value[1], ActivityTag));
            }
        }

        /// <summary>
        /// Cosntructor with camera Id and values
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="values"></param>
        public Activity(string cameraId,string values)
        { 
            //Separator , 
            char[] seperator = {','};
            string[] value = values.Split(seperator);

            //Converting the value to datetime
            InitDateValue(value[0]);
            
            //Initialising the cameraId
            this.CameraId = cameraId;
            //Intialising Visited Flag
            this.Visited = GetBoolValue(value[1]);
            //Intialising Falsed Flag
            this.Falsed = GetBoolValue(value[2]);
            //Initialiseing the activity type whether bookmark or activity
            this.ActivityType = value[4];
            this.Description = value[5];
            try
            {
                //get the duration
                int duration = Int32.Parse(value[3]);
                //Initialising Duration to the activity
                this.Duration = (duration / 60 == 0) ? 1 : duration / 60;
            }
            catch (Exception ex)
            {
                //If no duration, initialise to 1
                Logger.LogDebug(ex.StackTrace);
                this.Duration = 1;
            }
            try
            {
                if (value.Length > 6)
                {
                    this.MarkedInLive = Convert.ToBoolean(value[6]);
                }
                if (value.Length > 7)
                    this.ActivityTag = int.Parse(value[7]);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Activity(string cameraId,string values) " + ex.Message);
            }            
        }

        ///// <summary>
        ///// Cosntructor with camera Id and values
        ///// </summary>
        ///// <param name="cameraId"></param>
        ///// <param name="values"></param>
        //public Activity(string values)
        //{
        //    //Separator , 
        //    char[] seperator = { ',' };
        //    string[] value = values.Split(seperator);

        //    //Converting the value to datetime
        //    InitDateValue(value[0]);

        //    //Initialising the cameraId
        //    this.CameraId = value[6];
        //    //Intialising Visited Flag
        //    this.Visited = GetBoolValue(value[1]);
        //    //Intialising Falsed Flag
        //    this.Falsed = GetBoolValue(value[2]);
        //    //Initialiseing the activity type whether bookmark or activity
        //    this.ActivityType = value[4];
        //    if (value[5] == null || value[5].Equals("null"))
        //    {
        //        this.Description = "";
        //    }
        //    else
        //    {
        //        this.Description = value[5];
        //    }
        //    try
        //    {
        //        //get the duration
        //        int duration = Int32.Parse(value[3]);
        //        //Initialising Duration to the activity
        //        this.Duration = (duration / 60 == 0) ? 1 : duration / 60;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogDebug(ex.StackTrace);
        //        //If no duration, initialise to 1
        //        this.Duration = 1;
        //    }
        //}
        /// <summary>
        /// Initialising Date Value
        /// </summary>
        /// <param name="dateValue"></param>
        private void InitDateValue(string dateValue)
        {
            try
            {
                this.TimeStamp = DateTime.ParseExact(dateValue, "yyyy-MM-dd HH:mm:ss fff", null);
            }
            catch (Exception ex) 
            {
                Logger.LogDebug(ex.StackTrace);
                //
            }
        }

        /// <summary>
        /// Returns True/False by the string sent 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool GetBoolValue(string value)
        {
            //returns true if value =1 else false
            return value.Equals("1") ? true : false;
        }

        private bool isEscalated = false;

        public bool IsEscalated
        {
            get { return isEscalated; }
            set { isEscalated = value; }
        }


        /// <summary>
        /// Returns TimeStamp as string data type
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (ActivityType == Constants.IVigilConstants.BOOKMARK)
                return Description;
            
            // returns time of the day in string format
            return TimeStamp.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Equals Mehtod
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            //Determines whether the Activity Object is equal to the current Object.
            Activity activity = (Activity)obj;
            // returns true if equal
            return this.CameraId.Equals(activity.CameraId) && this.TimeStamp.Equals(activity.TimeStamp);
        }
    }
}
