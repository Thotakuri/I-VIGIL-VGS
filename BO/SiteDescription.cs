/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using I_vigil.Util;
using I_vigil.ProvigilService;

namespace I_vigil.BO
{
    public class SiteDescription
    {

        private string descriptionText;

        public string DescriptionText
        {
            get { return descriptionText; }
            set { descriptionText = value; }
        }

        private bool textChanged = false;

        public bool TextChanged
        {
            get { return textChanged; }
            set { textChanged = value; }
        }

        private Image descriptionImage;

        public Image DescriptionImage
        {
            get { return descriptionImage; }
            set { descriptionImage = value; }
        }

        private Image descriptionNightImage;

        public Image DescriptionNightImage
        {
            get { return descriptionNightImage; }
            set { descriptionNightImage = value; }
        }

        private bool imageChanged = false;

        public bool ImageChanged
        {
            get { return imageChanged; }
            set { imageChanged = value; }
        }

        private bool imageNightChanged = false;

        public bool ImageNightChanged
        {
            get { return imageNightChanged; }
            set { imageNightChanged = value; }
        }

        private bool isBoundaryChanged = false;

        public bool IsBoundaryChanged
        {
            get { return isBoundaryChanged; }
            set { isBoundaryChanged = value; }
        }
        private bool isMotionLighrChanged=false;
        public bool IsMotionLightChanged
        {
            get { return isMotionLighrChanged; }
            set { isMotionLighrChanged = value; }
        }

        //private List<Boundary> boundaryList = new List<Boundary>();
        Dictionary<string, Boundary> boundaryList = new Dictionary<string, Boundary>();

        public Dictionary<string, Boundary> BoundaryList
        {
            get { return boundaryList; }
            set { boundaryList = value; }
        }

        public void CreateBoundaries(string bdryString)
        {
            if (bdryString == null || bdryString.Length == 0)
                return;
            try
            {
                Logger.LogDebug("CreateBoundaries -- " + bdryString);
                string[] boundaries = bdryString.Split('^');

                for (int i = 0; i < boundaries.Length; i++)
                {
                    if (!boundaries[i].Contains(","))
                        break;
                    Boundary boundary = new Boundary();
                    boundary.DecodeBoundary(boundaries[i]);
                    BoundaryList.Add(boundary.BoundaryID, boundary);
                }
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.StackTrace + "\n" + ex.Message);
            }
        }

        public string GetBoundaryString()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (KeyValuePair<string, Boundary> kvp in BoundaryList)
                {
                    sb.Append(kvp.Value.EncodeBoundary());
                    sb.Append("^");
                }                
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.StackTrace + "\n" + ex.Message);
            }
            return sb.ToString();
        }

        #region Camera View Info part

        public bool AccessControlled { set; get; }
        public string CamDirection { set; get; }
        public string CameraId { set; get; }
        public bool ContainsEntrance { set; get; }
        public string EditedUser { set; get; }
        public string EditedTime { set; get; }
        public bool PublicallyAccessible { set; get; }
        public bool PublicRoadInView { set; get; }
        public string UnitLocation { set; get; }
        public bool KnownLighting { set; get; }              //added knownlighting
        public bool KnownWater { set; get; }                 //added knownwater
        public string MotionLightKits { set; get;}             //added motion light kits

        private CameraViewInfo _proxyObj;

        public bool IsCameraViewInfoChanged()
        {
            if (_proxyObj == null)
            {
                if (CameraId == null)
                    return false;
                else
                    return true;
            }

            if (_proxyObj.accessControlled == AccessControlled
                && _proxyObj.camDirection == CamDirection
                && _proxyObj.containsEntrance == ContainsEntrance
                && _proxyObj.publicallyAccessible == PublicallyAccessible
                && _proxyObj.publicRoadInView == PublicRoadInView
                && _proxyObj.unitLocation == UnitLocation
                &&_proxyObj.motionLightKits==MotionLightKits)
                return false;
            else
                return true;
        }

        public void AddCameraViewInfo(CameraViewInfo cameraViewInfo)
        {
            if (cameraViewInfo == null)
                return;

            _proxyObj = cameraViewInfo;

            AccessControlled = (_proxyObj.accessControlled == null) ? false : (bool)_proxyObj.accessControlled;
            CamDirection = _proxyObj.camDirection;
            CameraId = _proxyObj.cameraId;
            ContainsEntrance = (_proxyObj.containsEntrance == null) ? false : (bool)_proxyObj.containsEntrance;
            EditedTime = _proxyObj.editedTime;
            EditedUser = _proxyObj.editedUser;
            PublicallyAccessible = (_proxyObj.publicallyAccessible == null) ? false : (bool)_proxyObj.publicallyAccessible;
            PublicRoadInView = (_proxyObj.publicRoadInView == null) ? false : (bool) _proxyObj.publicRoadInView;
            UnitLocation = _proxyObj.unitLocation;
            KnownLighting = (_proxyObj.knownLighting == null) ? false : (bool)_proxyObj.knownLighting;   //added
            KnownWater = (_proxyObj.knownWater == null) ? false : (bool)_proxyObj.knownWater;            //added
            MotionLightKits = _proxyObj.motionLightKits;                                                 //added    
        }

        public CameraViewInfo GetCameraViewInfo()
        {
            CameraViewInfo cameraViewInfo = new CameraViewInfo();

            cameraViewInfo.accessControlled = AccessControlled;
            cameraViewInfo.camDirection = CamDirection;
            cameraViewInfo.cameraId = CameraId;
            cameraViewInfo.containsEntrance = ContainsEntrance;
            cameraViewInfo.editedUser = ((User)System.Windows.Application.Current.Properties["User"]).Username;
            cameraViewInfo.publicallyAccessible = PublicallyAccessible;
            cameraViewInfo.publicRoadInView = PublicRoadInView;
            cameraViewInfo.unitLocation = UnitLocation;
            cameraViewInfo.knownLighting = KnownLighting;    //added
            cameraViewInfo.knownWater = KnownWater;          //added
            cameraViewInfo.motionLightKits = MotionLightKits;//added

            return cameraViewInfo;
        }

        #endregion
    }
}
