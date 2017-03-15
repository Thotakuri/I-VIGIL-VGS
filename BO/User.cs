/* 
 * Provigil Surveillance Limited 
 */ 

using System.Collections.Generic;
using System.Diagnostics;
using System;
using I_vigil.Util;

namespace I_vigil.BO
{
    /// <summary>
    /// User Business Object to get the proxy user, roles and cameralist configured to the User
    /// </summary>
    public class User
    {
        //proxy User from webservice
        private ProvigilService.User _proxyUser = new I_vigil.ProvigilService.User();
        //cameraList 
        private List<Camera> cameraList = null;
        //userRights
        private List<string> userRights = null;

        

        private string accessInfo;

        public string AccessInfo
        {
            get { return accessInfo; }
            set { accessInfo = value; }
        }

        /// <summary>
        /// Get User Rights 
        /// </summary>
        internal List<string> UserRights
        {
            get { return userRights; }
            set { userRights = value; }
        }

        /// <summary>
        /// gets Camera List
        /// </summary>
        internal List<Camera> CameraList
        {
            get { return cameraList; }
            set { cameraList = value; }
        }


        /// <summary>
        /// Blank Constructor
        /// </summary>
        public User()
        {}
        
        /// <summary>
        /// Constructor for User
        /// </summary>
        /// <param name="_user"></param>
        public User(ProvigilService.User _user)
        {
            //initilise user
            _proxyUser =_user;
            //initialise camera list
            initCameraList();
            //Ger user Rights
            getUserRights();
            AccessInfo = _user.accessInfo;
        }

        /// <summary>
        /// Initalise CameraList 
        /// </summary>
        private void initCameraList()
        {
            //creating new cameralist instance
            cameraList = new List<Camera>();
            // Get all channels of user
            ProvigilService.ChannelConfiguration[] channelList  = _proxyUser.channelList;

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
        /// Return UserName
        /// </summary>
        public string Username
        {
            get{ return _proxyUser.userName;}
            set { _proxyUser.userName = value;}
        }

        /// <summary>
        /// Return UserName
        /// </summary>
        public string Password
        {
            get { return _proxyUser.password; }
            set { _proxyUser.password = value; }
            
        }


        /// <summary>
        /// Get User Rights from proxy user to current USER
        /// </summary>
        private void getUserRights()
        {
            //creating new user rights instance
            userRights = new List<string>();
            //check if user has ptz 
            if (_proxyUser.ptz)
                //Add PTZ to userRight list
                userRights.Add(Constants.IVigilConstants.PTZ);
            //check if user has open archive 
            if (_proxyUser.openArchive)
                //Add OPEN_ARCHIVE to userRight list
                userRights.Add(Constants.IVigilConstants.OPEN_ARCHIVE);
            //check if user has site information 
            if (_proxyUser.siteInfo)
                //Add SITE_INFO to userRight list
                userRights.Add(Constants.IVigilConstants.SITE_INFO);
            //check if user has archive player
            if (_proxyUser.archivePlayer)
                //Add ARCHIVE_PLAYER to userRight list
                userRights.Add(Constants.IVigilConstants.ARCHIVE_PLAYER);
            //check if user has croping
            if (_proxyUser.croping)
                //Add CROPING to userRight list
                userRights.Add(Constants.IVigilConstants.CROPING);
            //check if user has false marking
            if (_proxyUser.falseActivity)
                //Add FALSE_MARKING to userRight list
                userRights.Add(Constants.IVigilConstants.FALSE_MARKING);
            //check if user has visited activity
            if (_proxyUser.visitedActivity)
                //Add VISITED_ACTIVITY to userRight list
                userRights.Add(Constants.IVigilConstants.VISITED_ACTIVITY );
            //check if user has outputs
            if (_proxyUser.events)
                //Add OUTPUTS to userRight list
                userRights.Add(Constants.IVigilConstants.OUTPUTS);
            if (_proxyUser.activityAlert)
                //Add OUTPUTS to userRight list
                userRights.Add(Constants.IVigilConstants.RGTALERT);
            if (_proxyUser.exportActivity)
                //Add OUTPUTS to userRight list
                userRights.Add(Constants.IVigilConstants.RGTEXPORT);
            if (_proxyUser.settings)
                //Add OUTPUTS to userRight list
                userRights.Add(Constants.IVigilConstants.RGTSETTINGS);
            if (_proxyUser.snapShot)
                //Add OUTPUTS to userRight list
                userRights.Add(Constants.IVigilConstants.RGTSNAPSHOT);
            if (_proxyUser.snapShot)
                //Add OUTPUTS to userRight list
                userRights.Add(Constants.IVigilConstants.RGTSNAPSHOT);
            if (_proxyUser.bookMark)
                //Add OUTPUTS to userRight list
                userRights.Add(Constants.IVigilConstants.RGTBOOKMARK);
            if (_proxyUser.autoLogout)
            {
                userRights.Add(Constants.IVigilConstants.RGTAUTOLOGOUT);
                userRights.Add(_proxyUser.logoutTime.ToString());
            }
            try{
                if(_proxyUser.allowLiveMarking)
                    userRights.Add(Constants.IVigilConstants.RGTLIVEMARKING);
            }catch(Exception ex){
                Logger.LogDebug("Exception Getting User Role (Ativity marking on live) "+ex.Message);
            }
        }


        /// <summary>
        /// Get User Rights from proxy user to current USER
        /// </summary>
        public string[] getAdminUserRights()
        {
            List<string> adminUserRights = new List<string>();
            //creating new user rights instance
            adminUserRights = new List<string>();
            //check if user has ptz 
            //Add PTZ to userRight list
            adminUserRights.Add(Constants.IVigilConstants.PTZ);
            //check if user has open archive 
            //Add OPEN_ARCHIVE to userRight list
            adminUserRights.Add(Constants.IVigilConstants.OPEN_ARCHIVE);
            //check if user has site information 
            //Add SITE_INFO to userRight list
            adminUserRights.Add(Constants.IVigilConstants.SITE_INFO);
            //check if user has archive player
            //Add ARCHIVE_PLAYER to userRight list
            adminUserRights.Add(Constants.IVigilConstants.ARCHIVE_PLAYER);
            //check if user has croping
            //Add CROPING to userRight list
            adminUserRights.Add(Constants.IVigilConstants.CROPING);
            //check if user has false marking
            //Add FALSE_MARKING to userRight list
            adminUserRights.Add(Constants.IVigilConstants.FALSE_MARKING);
            //check if user has visited activity
            //Add VISITED_ACTIVITY to userRight list
            adminUserRights.Add(Constants.IVigilConstants.VISITED_ACTIVITY);
            //check if user has outputs
            //Add OUTPUTS to userRight list
            adminUserRights.Add(Constants.IVigilConstants.OUTPUTS);
            //Add OUTPUTS to userRight list
            adminUserRights.Add(Constants.IVigilConstants.RGTALERT);
            //Add OUTPUTS to userRight list
            adminUserRights.Add(Constants.IVigilConstants.RGTEXPORT);
            //Add OUTPUTS to userRight list
            adminUserRights.Add(Constants.IVigilConstants.RGTSETTINGS);
            //Add OUTPUTS to userRight list
            adminUserRights.Add(Constants.IVigilConstants.RGTSNAPSHOT);
            //Add OUTPUTS to userRight list
            adminUserRights.Add(Constants.IVigilConstants.RGTSNAPSHOT);
            //Add OUTPUTS to userRight list
            adminUserRights.Add(Constants.IVigilConstants.RGTBOOKMARK);
            adminUserRights.Add(Constants.IVigilConstants.RGTLIVEMARKING);

            return adminUserRights.ToArray();
        }
    }
}
