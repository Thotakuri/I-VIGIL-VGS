/* 
 * Provigil Surveillance Limited 
 */ 


namespace I_vigil.BO
{
    public class ServerConfig
    {
        //proxy server config object
        private ProvigilService.ServerConfig _proxyServerConfig = new I_vigil.ProvigilService.ServerConfig();

        /// <summary>
        /// ServerConfig constructor with proxy server object
        /// </summary>
        /// <param name="serverConfig"></param>
        public ServerConfig(ProvigilService.ServerConfig serverConfig)
        {
            //setting server config object
            _proxyServerConfig = serverConfig;            
        }

        public ServerConfig()
        {            
        }

        /// <summary>
        /// Get the archive days configured in server
        /// </summary>
        /// <returns>int</returns>
        public int GetArchiveDays()
        {
            //if proxy server object is not null then
            //return number of archive days
            if (_proxyServerConfig != null)
                return _proxyServerConfig.archiveDays;
            return -1;
        }

        /// <summary>
        /// This function get the status of multiple site configuration
        /// </summary>
        /// <returns>bool</returns>
        public bool IsServer()
        {
            //if proxy server object is not null then
            //return number of archive days
            if (_proxyServerConfig != null)
                return _proxyServerConfig.multipleServer;                  
            return false;            
        }

        /// <summary>
        /// Return Server Name
        /// </summary>
        public string ServerName
        {
            get { return _proxyServerConfig.siteName; }
            set { _proxyServerConfig.siteName = value; }
        }

        /// <summary>
        /// Return Repository Path
        /// </summary>
        public string RepositoryPath
        {
            get { return _proxyServerConfig.repositoryPath ; }
            set { _proxyServerConfig.repositoryPath = value; }
        }

        /// <summary>
        /// Return ArchivePath 
        /// </summary>
        public string ArchivePath
        {
            get { return _proxyServerConfig.archivePath ; }
            set { _proxyServerConfig.archivePath = value; }
        }

        /// <summary>
        /// Return ArchivePath 
        /// </summary>
        public bool Archive
        {
            get { return _proxyServerConfig.archive ; }
            set { _proxyServerConfig.archive = value; }
        }

        /// <summary>
        /// Return ActiveDays 
        /// </summary>
        public int ArchiveDays
        {
            get { return _proxyServerConfig.archiveDays ; }
            set { _proxyServerConfig.archiveDays  = value; }
        }

      
        /// <summary>
        /// Return ActiveDays 
        /// </summary>
        public bool MultipleServer
        {
            get { return _proxyServerConfig.multipleServer ; }
            set { _proxyServerConfig.multipleServer = value; }
        }


        /// <summary>
        /// Return boolean 
        /// </summary>
        public bool isCrm
        {
           get { return _proxyServerConfig.crm; }
           set { _proxyServerConfig.crm = value; }
        }
    }
}
