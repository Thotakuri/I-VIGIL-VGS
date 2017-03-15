/* 
 * Provigil Surveillance Limited 
 */ 


namespace I_vigil.BO
{
    public class ContactInfo
    {
        //proxy server config object
        private ProvigilService.ContactInfo _proxyContactInfo = new I_vigil.ProvigilService.ContactInfo();

        /// <summary>
        /// ContactInfo constructor with proxy server object
        /// </summary>
        /// <param name="serverConfig"></param>
        public ContactInfo(ProvigilService.ContactInfo contactInfo)
        {
            //setting server config object
            _proxyContactInfo = contactInfo;            
        }

        public ContactInfo()
        {            
        }

        /// <summary>
        /// Return Account Id
        /// </summary>
        public int AccountId
        {
            get { return _proxyContactInfo.accountId ; }
            set { _proxyContactInfo.accountId  = value; }
        }

        /// <summary>
        /// Return Potential Id
        /// </summary>
        public int PotentialId
        {
            get { return _proxyContactInfo.potentialId; }
            set { _proxyContactInfo.potentialId  = value; }
        }

        /// <summary>
        /// Return Company
        /// </summary>
        public string Company
        {
            get { return _proxyContactInfo.company; }
            set { _proxyContactInfo.company = value; }
        }

        /// <summary>
        /// Return Address
        /// </summary>
        public string Address
        {
            get { return _proxyContactInfo.address; }
            set { _proxyContactInfo.address  = value; }
        }

        /// <summary>
        /// Return City
        /// </summary>
        public string City
        {
            get { return _proxyContactInfo.city; }
            set { _proxyContactInfo.city = value; }
        }

        /// <summary>
        /// Return State
        /// </summary>
        public string State
        {
            get { return _proxyContactInfo.state; }
            set { _proxyContactInfo.state = value; }
        }
             

        /// <summary>
        /// Return Local police No
        /// </summary>
        public string LocalPoliceNo
        {
            get { return _proxyContactInfo.localPoliceNo ; }
            set { _proxyContactInfo.localPoliceNo  = value; }
        }

        /// <summary>
        /// Return InternetSource
        /// </summary>
        public string InternetSource
        {
            get { return _proxyContactInfo.internetSource ; }
            set { _proxyContactInfo.internetSource = value; }
        }

        /// <summary>
        /// Return description
        /// </summary>
        public string Description
        {
            get { return _proxyContactInfo.description; }
            set { _proxyContactInfo.description = value; }
        }

        /// <summary>
        /// Return Site
        /// </summary>
        public string Site
        {
            get { return _proxyContactInfo.site ; }
            set { _proxyContactInfo.site = value; }
        }

        /// <summary>
        /// Return MonitoringHours
        /// </summary>
        public string MonitoringHours
        {
            get { return _proxyContactInfo.monitoringHours ; }
            set { _proxyContactInfo.monitoringHours  = value; }
        }

        /// <summary>
        /// Return AddMonitoringHours
        /// </summary>
        public string AddMonitoringHours
        {
            get { return _proxyContactInfo.addMonitoringHours ; }
            set { _proxyContactInfo.addMonitoringHours = value; }
        }

        /// <summary>
        /// Return Emg Contact1
        /// </summary>
        public string EmgContact1
        {
            get { return _proxyContactInfo.emgContact1 ; }
            set { _proxyContactInfo.emgContact1 = value; }
        }
       

        /// <summary>
        /// Return Emg Contact1
        /// </summary>
        public string EmgContact2
        {
            get { return _proxyContactInfo.emgContact2; }
            set { _proxyContactInfo.emgContact2 = value; }
        }


        /// <summary>
        /// Return safe word
        /// </summary>
        public string SafeWord
        {
            get { return _proxyContactInfo.safeWord ; }
            set { _proxyContactInfo.safeWord = value; }
        }

        /// <summary>
        /// Return email
        /// </summary>
        public string Email
        {
            get { return _proxyContactInfo.email ; }
            set { _proxyContactInfo.email  = value; }
        }

        /// <summary>
        /// Return CrossStreet1
        /// </summary>
        public string CrossStreet1
        {
            get { return _proxyContactInfo.crossStreet1 ; }
            set { _proxyContactInfo.crossStreet1= value; }
        }

        /// <summary>
        /// Return CrossStreet2
        /// </summary>
        public string CrossStreet2
        {
            get { return _proxyContactInfo.crossStreet2; }
            set { _proxyContactInfo.crossStreet2 = value; }
        }

        /// <summary>
        /// Return ContactOwnerFirst
        /// </summary>
        public string ContactOwnerFirst
        {
            get { return _proxyContactInfo.contactOwnerFirst  ; }
            set { _proxyContactInfo.contactOwnerFirst  = value; }
        }

        /// <summary>
        /// Return SiteName
        /// </summary>
        public string SiteName
        {
            get { return _proxyContactInfo.siteName; }
            set { _proxyContactInfo.siteName = value; }
        }

        /// <summary>
        /// Return ZipCode
        /// </summary>
        public string ZipCode
        {
            get { return _proxyContactInfo.zipCode ; }
            set { _proxyContactInfo.zipCode  = value; }
        }

        /// <summary>
        /// Return Latitude
        /// </summary>
        public string Latitude
        {
            get { return _proxyContactInfo.latitude; }
            set { _proxyContactInfo.latitude = value; }
        }

        /// <summary>
        /// Return Langitude
        /// </summary>
        public string Langitude
        {
            get { return _proxyContactInfo.langitude; }
            set { _proxyContactInfo.langitude = value; }
        }

        public string SSS
        {
            get { return _proxyContactInfo.sss; }
            set { _proxyContactInfo.sss = value; }
        }

        public string SssContact
        {
            get { return _proxyContactInfo.sssContact; }
            set { _proxyContactInfo.sssContact = value; }
        }

        public string OutageNotificationMethod
        {
            get { return _proxyContactInfo.outageNotificationMethod; }
            set { _proxyContactInfo.outageNotificationMethod = value; }
        }

        public string PoliceDeptName
        {
            get { return _proxyContactInfo.policeDeptName; }
            set { _proxyContactInfo.policeDeptName = value; }
        }

        public string Occupied
        {
            get { return _proxyContactInfo.occupied; }
            set { _proxyContactInfo.occupied = value; }
        }


    }
}
