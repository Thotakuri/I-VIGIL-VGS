/* 
 * Provigil Surveillance Limited 
 */ 


namespace I_vigil.BO
{
    public class SensorConnectionInfo
    {
        //Sensor Configuration Object
        private ProvigilService.SensorConnectionInfo _proxySensorConfigObj = new I_vigil.ProvigilService.SensorConnectionInfo();

        public SensorConnectionInfo()
        {
        }

         /// <summary>
        /// ServerConfig constructor with proxy server object
        /// </summary>
        /// <param name="serverConfig"></param>
        public SensorConnectionInfo(ProvigilService.SensorConnectionInfo sensorConnInfo)
        {
            //setting server config object
            _proxySensorConfigObj = sensorConnInfo;            
        }

        /// <summary>
        /// Get Comport No
        /// </summary>
        public string ComNo
        {
            get { return _proxySensorConfigObj.comNo ; }
            set { _proxySensorConfigObj.comNo = value; }
        }

        /// <summary>
        /// Get Frequency
        /// </summary>
        public int Frequency
        {
            get { return _proxySensorConfigObj.frequency; }
            set { _proxySensorConfigObj.frequency  = value; }
        }

        /// <summary>
        /// Get Frequency
        /// </summary>
        public bool Enabled
        {
            get { return _proxySensorConfigObj.enabled; }
            set { _proxySensorConfigObj.enabled = value; }
        }

    }
}
