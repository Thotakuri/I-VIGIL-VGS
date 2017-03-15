/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using I_vigil.Util;
using System.Diagnostics;

namespace I_vigil.BO
{
    public class OutputEvent
    {

        # region "Declarations"

        private string cameraId;

        public string CameraId
        {
            get { return cameraId; }
            set { cameraId = value; }
        }
        private string cameraName;

        public string CameraName
        {
            get { return cameraName; }
            set { cameraName = value; }
        }
        private DateTime timeStamp;

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        private string outputTime;

        public string OutputTime
        {
            get { return outputTime; }
            set { outputTime = value; }
        }

        private string eventValue;

        public string EventValue
        {
            get { // returns time of the day in string format
                return CameraId + "," + TimeStamp.ToString("yyyy-MM-dd HH:mm:ss fff"); 
            }
            set { eventValue = value; }
        }

        private Activity activity;

        public Activity Activity
        {
            get { return activity; }
            set { activity = value; }
        }

        #endregion

        #region "Constructor"
        public OutputEvent(Activity activity)
        {
            this.Activity = activity;
            this.CameraId = activity.CameraId;
            this.CameraName = CameraUtil.GetInstance().GetCameraName(this.CameraId);
            try
            {
                this.TimeStamp = activity.TimeStamp;
            }
            catch (Exception ex) 
            {
                Logger.LogDebug(ex.StackTrace);
                //
            }
            this.OutputTime = TimeStamp.ToString("HH:mm:ss");
        }
        #endregion

        #region "Overriding the object"
        /// <summary>
        /// Returns TimeStamp as string data type
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // returns time of the day in string format
            return CameraId + "," + TimeStamp.ToString("yyyy-MM-dd HH:mm:ss fff");
        }
        #endregion

    }
}
