/* 
 * Provigil Surveillance Limited 
 */

using I_vigil.Configuration.JsonConfigurations;
using System;
using System.Collections.Generic;

namespace I_vigil.BO
{
    /*
     * Site Information Class
     */
    public class SiteInfo
    {
        //Site Infomration Object
        private ProvigilService.SiteDetails _proxySiteInfo = new I_vigil.ProvigilService.SiteDetails();

        //cameraList 
        private List<Camera> cameraList = new List<Camera>();
        //output list
        private List<OutputDevice> outputList = new List<OutputDevice>();

        ///<summary>
        /// Gets the Site Name
        /// </summary>
        public List<Camera> ChannelList
        {
            get { return cameraList; }
            set { cameraList = value; }
        }

        /// <summary>
        /// Gets the Site Name
        /// </summary>
        public List<OutputDevice> OutputList
        {
            get { return outputList; }
            set { outputList = value; }
        }

        //longitude
        private string _longitude;
        //Latitude
        private string _latitude;
        
        /// <summary>
        /// Blank Constructor
        /// </summary>
        public SiteInfo()
        {
        }
        
        //Constructor site information Object
        public SiteInfo(ProvigilService.SiteDetails siteInfo)
        {
            _proxySiteInfo = siteInfo;
            siteName = siteInfo.siteName;
            potentialId = siteInfo.potentialId;
            //Get CameraList
            GetCameraList();
            //Get output List
            GetOutputList();
        }

        public SiteInfo(IvigilJsonList siteInfo)
        {
            siteName = siteInfo.SiteName;
            potentialId =Convert.ToInt32(siteInfo.potentialId);
            GetJsonCameraList(siteInfo.cameraList);
            GetJsonOutputList(siteInfo.outputList);
            //potentialId =Convert.To siteInfo.potentialId;

        }

        /// <summary>
        /// Get CameraList 
        /// </summary>
        private void GetCameraList()
        {
            //creating new cameralist instance
            cameraList = new List<Camera>();
            // Get all channels of user
            ProvigilService.ChannelConfiguration[] channelList = _proxySiteInfo.channelList;           

            if (channelList != null)
            {
                //Adding channels to the Camera
                foreach (ProvigilService.ChannelConfiguration channel in channelList)
                {
                    if (channel == null) return;

                    //New instance of camera Object
                    Camera cam = new Camera(channel);
                    //Adding Camera to the camera list
                    cameraList.Add(cam);
                }
            }
        }

        /// <summary>
        /// Get CameraList 
        /// </summary>
        private void GetOutputList()
        {
            //creating new cameralist instance
            outputList  = new List<OutputDevice>();
            // Get all channels of user
            ProvigilService.AlarmDevice[]  alarmList = _proxySiteInfo.outputList;

            // check for null values
            if (alarmList == null) return;

            //Adding channels to the Camera
            foreach (ProvigilService.AlarmDevice alaram in alarmList)
            {
                if (alaram == null) continue;

                //New instance of camera Object
                OutputDevice alarm = new OutputDevice(alaram);
                //Adding Camera to the camera list
                outputList.Add(alarm);
            }
        }

        private void GetJsonCameraList(List<I_vigil.Configuration.JsonConfigurations.IvigilJsonList.jsonlist> cameralist )
        {
            //creating new cameralist instance
            cameraList = new List<Camera>();
         
            if (cameralist == null)
                return;

            foreach (I_vigil.Configuration.JsonConfigurations.IvigilJsonList.jsonlist camera in cameralist)
            {
                if (camera == null)
                    return;
                Camera cam = new Camera(camera);
                cameraList.Add(cam);
            }


        }
        private void GetJsonOutputList(List<I_vigil.Configuration.JsonConfigurations.IvigilJsonList.jsonalarmlist> outputlist)
        {
            outputList = new List<OutputDevice>();
            if (outputlist == null)
                return;

            foreach (I_vigil.Configuration.JsonConfigurations.IvigilJsonList.jsonalarmlist output in outputlist)
            {
                if (output == null)
                    return;
                OutputDevice oput = new OutputDevice(output);
                outputList.Add(oput);
            }

        }
        private string siteName;
        /// <summary>
        /// Gets the Site Name
        /// </summary>
       	public string SiteName
        { 
            get {return siteName;}
            set { siteName = value; }
        }

		
        /// <summary>
        /// Gets the AccountId
        /// </summary>
		public int AccountId  
        { 
            get {return _proxySiteInfo.accountId;}
            set { _proxySiteInfo.accountId = value;}
        }


        private int potentialId;
        /// <summary>
        /// Gets the Potential Id
        /// </summary>
		public int PotentialId 
        { 
            get {return potentialId;}
            set { potentialId = value;}
        }
        /// <summary>
        /// Gets the Company Name
        /// </summary>
		public string Company 
        { 
            get {return _proxySiteInfo.company;}
            set { _proxySiteInfo.company = value; }
        }
		/// <summary>
        /// Gets the Address
		/// </summary>
		public string Address 
        { 
            get {return _proxySiteInfo.address;}
            set { _proxySiteInfo.address = value; } 
        }
		/// <summary>
        /// Gets the City
		/// </summary>
		public string City 
        { 
            get {return _proxySiteInfo.city;}
            set { _proxySiteInfo.city = value; } 
        }
		/// <summary>
		/// Gets the State
		/// </summary>
		public string State 
        { 
            get {return _proxySiteInfo.state;}
            set { _proxySiteInfo.state = value; } 
        }
		/// <summary>
        /// Gets the local police Number
		/// </summary>
		public string LocalPoliceNo 
        { 
            get {return _proxySiteInfo.localPoliceNo;}
            set { _proxySiteInfo.localPoliceNo = value; }
        }
		/// <summary>
        /// Gets the Internet Source
		/// </summary>
		public string InternetSource 
        { 
            get {return _proxySiteInfo.internetSource;}
            set { _proxySiteInfo.internetSource = value; }
        }

        /// <summary>
        /// Gets the Description
        /// </summary>
		public string Description 
        { 
            get {return _proxySiteInfo.description;}
            set { _proxySiteInfo.description = value; }
        }
		/// <summary>
        /// Gets the Site Address
		/// </summary>
		public string Site 
        { 
            get {return _proxySiteInfo.site;}
            set { _proxySiteInfo.site = value; }
        }
		/// <summary>
        /// Gets the Monitoring Hours
		/// </summary>
		public string MonitoringHours 
        { 
            get {return _proxySiteInfo.monitoringHours;}
            set { _proxySiteInfo.monitoringHours = value;}
        }
		/// <summary>
        /// Gets the add monitoring hours
		/// </summary>
		public string AddMonitoringHours 
        { 
            get {return _proxySiteInfo.addMonitoringHours;}
            set { _proxySiteInfo.addMonitoringHours = value; }
        }
		
        /// <summary>
        /// Gets the Emg Contact 1
        /// </summary>
		public string EmgContact1 
        { 
            get {return _proxySiteInfo.emgContact1;}
            set { _proxySiteInfo.emgContact1 = value;}
        }
		/// <summary>
        /// Gets the Emg Contact 2
		/// </summary>
		public string EmgContact2 
        { 
            get {return _proxySiteInfo.emgContact2;}
            set { _proxySiteInfo.emgContact2 = value; }
        }
		/// <summary>
		/// Gets the SafeWord
		/// </summary>
		public string SafeWord 
        { 
            get {return _proxySiteInfo.safeWord;}
            set { _proxySiteInfo.safeWord = value; }
        }
		/// <summary>
        /// Gets the Email
		/// </summary>
		public string Email 
        { 
            get {return _proxySiteInfo.email;}
            set { _proxySiteInfo.email = value; }
        }
		
        /// <summary>
        /// Gets the Street Address 1
        /// </summary>
		public string CrossStreet1 
        { 
            get {return _proxySiteInfo.crossStreet1;}
            set { _proxySiteInfo.crossStreet1 = value; }
        }
		/// <summary>
        /// Gets the street Address 2
		/// </summary>
		public string CrossStreet2 
        { 
            get {return _proxySiteInfo.crossStreet2;}
            set { _proxySiteInfo.crossStreet2 = value; }
        }
		/// <summary>
        /// Gets the Contact Owner
		/// </summary>
		public string ContactOwnerFirst 
        { 
            get {return _proxySiteInfo.contactOwnerFirst;}
            set { _proxySiteInfo.contactOwnerFirst = value; }
        }
        /// <summary>
        /// Gets the Zip Code
        /// </summary>
        public string ZipCode
        {            
            get { return _proxySiteInfo.zipCode; }
            set { _proxySiteInfo.zipCode = value; }
        }

        
        /// <summary>
        /// Gets the longitude
        /// </summary>
        public string Longitude
        {
            get { return _proxySiteInfo.langitude; }
            set { _longitude = value; }
        }


        /// <summary>
        /// Gets the Latitude
        /// </summary>
        public string Latitude
        {
            get { return _proxySiteInfo.latitude; }
            set { _latitude = value; }
        }

        /// <summary>
        /// Gets the Latitude
        /// </summary>
        public bool StopCaptureMonitoringHours
        {
            get { return _proxySiteInfo.stopCaptureMonitoringHours; }
            set { _proxySiteInfo.stopCaptureMonitoringHours = value; }
        }

        public string SSS
        {
            get { return _proxySiteInfo.sss; }
            set { _proxySiteInfo.sss = value; }
        }

        public string SssContact
        {
            get { return _proxySiteInfo.sssContact; }
            set { _proxySiteInfo.sssContact = value; }
        }

        public string OutageNotificationMethod
        {
            get { return _proxySiteInfo.outageNotificationMethod; }
            set { _proxySiteInfo.outageNotificationMethod = value; }
        }

        public string Occupied
        {
            get { return _proxySiteInfo.occupied; }
            set { _proxySiteInfo.occupied = value; }
        }

        public string PoliceDeptName
        {
            get { return _proxySiteInfo.policeDeptName; }
            set { _proxySiteInfo.policeDeptName = value; }
        }

        public string TimeZone
        {
            get { return _proxySiteInfo.timezone; }
        }

        public bool IsEventMonitoring
        {
            get
            {
                try { return _proxySiteInfo.eventMonitoring; }
                catch { }
                return false;
            }
        }

        public string UnitId
        {
            get
            {
                try { return _proxySiteInfo.unitId; }
                catch { }
                return "";
            }
        }

        public string AlarmBusinessLicense
        {
            get
            {
                try { return _proxySiteInfo.alarmBusinessLicense; }
                catch { }
                return "";
            }
        }

        public string AlarmCustomerLicense
        {
            get
            {
                try { return _proxySiteInfo.alarmCustomerLicense; }
                catch { }
                return "";
            }
        }

        public string FieldServiceRep
        {
            get
            {
                try { return _proxySiteInfo.fieldServiceRep; }
                catch { }
                return "";
            }
        }

        public string GateCode
        {
            get
            {
                try { return _proxySiteInfo.gateCode; }
                catch { }
                return "";
            }
        }

        public string GuardTourTimes
        {
            get
            {
                try { return _proxySiteInfo.guardTourTimes; }
                catch { }
                return "";
            }
        }

        public string MonitoredCameras
        {
            get
            {
                try { return _proxySiteInfo.monitoredCameras; }
                catch { }
                return "";
            }
        }

        public string MonitoringService
        {
            get
            {
                try { return _proxySiteInfo.monitoringService; }
                catch { }
                return "";
            }
        }
    }
}
