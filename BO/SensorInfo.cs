/* 
 * Provigil Surveillance Limited 
 */ 


namespace I_vigil.BO
{
    public class SensorInfo
    {
        //Sensor Configuration Object
        private ProvigilService.SensorInfo _proxySensorObj = new I_vigil.ProvigilService.SensorInfo();

        public SensorInfo()
        {
        }
         /// <summary>
        /// Constructor with sound Configuration Object
        /// </summary>
        /// <param name="soundObj"></param>
        public SensorInfo(ProvigilService.SensorInfo sensorObj)
        {
            _proxySensorObj = sensorObj;
        }


        /// <summary>
        /// Get SensorNo
        /// </summary>
        public string SensorNo
        {
            get { return _proxySensorObj.sensorNo; }
            set { _proxySensorObj.sensorNo = value; }
        }

        /// <summary>
        /// Get SensorNo
        /// </summary>
        public string Message
        {
            get { return _proxySensorObj.message ; }
            set { _proxySensorObj.message = value; }
        }




    }
}
