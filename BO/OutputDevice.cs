/* 
 * Provigil Surveillance Limited 
 */ 


namespace I_vigil.BO
{
    /*
     * Output device Class
     */
    public class OutputDevice
    {
        //Alarm Device Object
        private ProvigilService.AlarmDevice _proxyOutputObj = new I_vigil.ProvigilService.AlarmDevice();
        //Status of the alarm device
        private bool _status = false;

        /// <summary>
        /// Gets or Sets the Status
        /// </summary>
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// Blank Constructor
        /// </summary>
        public OutputDevice()
        { 
        }

        /// <summary>
        /// Cosntructor wirh alarm device object
        /// </summary>
        /// <param name="outputObj"></param>
        public OutputDevice(ProvigilService.AlarmDevice outputObj)
        {
            _proxyOutputObj = outputObj;
            displayName = outputObj.name;
            Status = outputObj.status;
        }

        public OutputDevice(I_vigil.Configuration.JsonConfigurations.IvigilJsonList.jsonalarmlist output)
        {
            displayName = output.displayName;
        }

        /// <summary>
        /// Get alarm device name
        /// </summary>
        public string Name
        {
            get{ return _proxyOutputObj.name;}
            set { _proxyOutputObj.name = value;}
        }


        private string displayName;
        /// <summary>
        /// Get Alarm Device Name
        /// </summary>
        public string DisplayName
        {
            get{ return displayName;}
            set { displayName = value; } 
        }
        /// <summary>
        /// Get Alarm Device Type
        /// </summary>
        public string Type
        {
            get{ return _proxyOutputObj.type;}
            set { _proxyOutputObj.type = value; } 
        }

        /// <summary>
        /// Overriding the ToString method
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            //return the display name string
            return _proxyOutputObj.displayName;
        }

        /// <summary>
        /// To get the siteName
        /// </summary>
        /// <returns></returns>
        public string SiteName
        {
            get { return _proxyOutputObj.siteName; }
            set { _proxyOutputObj.siteName = value; }            
        }

        /// <summary>
        /// To get the address
        /// </summary>
        /// <returns></returns>
        public string Address
        {
            get { return _proxyOutputObj.address; }
            set { _proxyOutputObj.address = value; }
        }

        /// <summary>
        /// To get the address
        /// </summary>
        /// <returns></returns>
        public string ComportNo
        {
            get { return _proxyOutputObj.comportNo ; }
            set { _proxyOutputObj.comportNo = value; }
        }

        /// <summary>
        /// To get the address
        /// </summary>
        /// <returns></returns>
        public string DeviceType
        {
            get { return _proxyOutputObj.deviceType ; }
            set { _proxyOutputObj.deviceType = value; }
        }


        /// <summary>
        /// To get the address
        /// </summary>
        /// <returns></returns>
        public string IpAddress
        {
            get { return _proxyOutputObj.ipAddress ; }
            set { _proxyOutputObj.ipAddress = value; }
        }

        /// <summary>
        /// To get the address
        /// </summary>
        /// <returns></returns>
        public int Port
        {
            get { return _proxyOutputObj.port ; }
            set { _proxyOutputObj.port = value; }
        }

        /// <summary>
        /// To get the address
        /// </summary>
        /// <returns></returns>
        public string Password
        {
            get { return _proxyOutputObj.password; }
            set { _proxyOutputObj.password = value; }
        }

        /// <summary>
        /// To get the address
        /// </summary>
        /// <returns></returns>
        public string UserName
        {
            get { return _proxyOutputObj.userName; }
            set { _proxyOutputObj.userName = value; }
        }
    }
}
