/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace I_vigil.BO
{
    public class LightDevice
    {
        public LightDevice()
        {
        }

        public LightDevice(ProvigilService.LightDevice device)
        {
            _deviceAddress = device.deviceAddress;
            _displayName = device.displayName;
            _ipAddress = device.ipAddress;
            _lightId = device.lightId;
            _logicalStatus = device.logicalStatus;
            _password = device.password;
            _port = device.port;
            _siteName = device.siteId;
            _startHour = device.startHour;
            _startMin = device.startMin;
            _status = device.status;
            _stopHour = device.stopHour;
            _stopMin = device.stopMin;
            _userAction = device.userAction;
            _userName = device.userName;
            _type = device.type;
        }

        private string _deviceAddress = "";

        public string DeviceAddress
        {
            get { return _deviceAddress; }
            set { _deviceAddress = value; }
        }

        private string _displayName = "";

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private string _ipAddress = "";

        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        private int _lightId;

        public int LightId
        {
            get { return _lightId; }
            set { _lightId = value; }
        }

        private bool _logicalStatus;

        public bool LogicalStatus
        {
            get { return _logicalStatus; }
            set { _logicalStatus = value; }
        }

        private string _password = "";

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private int _port;

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private string _siteName = "";

        public string SiteName
        {
            get { return _siteName; }
            set { _siteName = value; }
        }

        private int _startHour;

        public int StartHour
        {
            get { return _startHour; }
            set { _startHour = value; }
        }

        private int _startMin;

        public int StartMin
        {
            get { return _startMin; }
            set { _startMin = value; }
        }

        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int _stopHour;

        public int StopHour
        {
            get { return _stopHour; }
            set { _stopHour = value; }
        }

        private int _stopMin;

        public int StopMin
        {
            get { return _stopMin; }
            set { _stopMin = value; }
        }

        private bool _userAction;

        public bool UserAction
        {
            get { return _userAction; }
            set { _userAction = value; }
        }

        private string _userName = "";

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _type = "";

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public ProvigilService.LightDevice GetProvigilLightDevice()
        {
            ProvigilService.LightDevice device = new ProvigilService.LightDevice();

            device.deviceAddress = _deviceAddress;
            device.displayName = _displayName;
            device.ipAddress = _ipAddress;
            device.lightId = _lightId;
            device.logicalStatus = _logicalStatus;
            device.password = _password;
            device.port = _port;
            device.siteId = _siteName;
            device.startHour = _startHour;
            device.startMin = _startMin;
            device.status = _status;
            device.stopHour = _stopHour;
            device.stopMin = _stopMin;
            device.userAction = _userAction;
            device.userName = _userName;
            device.type = _type;

            return device;
        }
    }
}
