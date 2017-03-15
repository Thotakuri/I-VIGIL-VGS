/* 
 * Provigil Surveillance Limited 
 */ 

using I_vigil.Properties;

namespace I_vigil.BO
{
    public class IvigilConfig
    {
        private string _consoleUrl = Settings.Default.ChatServerEndPoint;

        public string ConsoleUrl
        {
            get { return _consoleUrl; }
            set { _consoleUrl = value; }
        }

        private string _workspaceUrl = Settings.Default.AuditServerEndPoint;

        public string WorkspaceUrl
        {
            get { return _workspaceUrl; }
            set { _workspaceUrl = value; }
        }

        private string _organisation = "Pro-Vigil";

        public string Organisation
        {
            get { return _organisation; }
            set { _organisation = value; }
        }

        public IvigilConfig(ProvigilService.IvigilConfig config)
        {
            _organisation = config.org;
            if (config.consoleURL.Contains(Constants.IVigilConstants.CONSOLE_URL_EXTENSION))
                _consoleUrl = config.consoleURL;
            else
                _consoleUrl = config.consoleURL + Constants.IVigilConstants.CONSOLE_URL_EXTENSION;

            if (config.workspaceURL.Contains(Constants.IVigilConstants.WORKSPACE_URL_EXTENSION))
                _workspaceUrl = config.workspaceURL;
            else
                _workspaceUrl = config.workspaceURL + Constants.IVigilConstants.WORKSPACE_URL_EXTENSION;
        }

        public IvigilConfig()
        {
        }
    }
}
