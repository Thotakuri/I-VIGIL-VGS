/* 
 * Provigil Surveillance Limited 
 */ 


namespace I_vigil.BO
{
    /*
     * Trigger Event Response
     */
    public class TriggerEventResponse
    {
        //Trigger Response Object Instance
        private ProvigilService.TriggerResponse _proxyEventResp = null;

        /// <summary>
        /// Blank Constructor
        /// </summary>
        public TriggerEventResponse()
        { 
        }

        /// <summary>
        /// Constructor with Trigger Response Object parameter
        /// </summary>
        /// <param name="eventResponse"></param>
        public TriggerEventResponse(ProvigilService.TriggerResponse eventResponse)
        {
            _proxyEventResp = eventResponse; 
        }
        /// <summary>
        /// Gets the DeviceId
        /// </summary>
        public string DeviceId
        {
            get { return _proxyEventResp.deviceId; } 
        }
        /// <summary>
        /// Gets the Type
        /// </summary>
        public string Type
        {
            get { return _proxyEventResp.type; }
        }
        /// <summary>
        /// Gets the Event Status
        /// </summary>
        public bool Status
        {
            get { return _proxyEventResp.status; }
        }

        public int ResponseType
        {
            get { return _proxyEventResp.responseType; }
        }
        
    }
}
