/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Drawing;
using I_vigil.Util;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using I_vigil.Configuration.JsonConfigurations;
using I_vigil.MotionAnalytics.BO;
using System.Windows;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Shapes;

namespace I_vigil.BO
{
    /*
     * Camera Class
     * */
    public class Camera
    {
        //proxy Channel 
        ProvigilService.ChannelConfiguration _proxyChannel = new I_vigil.ProvigilService.ChannelConfiguration();
        //mask values for drawing rectangle
        private System.Drawing.Rectangle[] _maskValues = null;
        int _width = 320;
        int _height = 240;

        string tripWire = null;
        string maskin = null;

        /// <summary>
        /// gets and sets mask values
        /// </summary>
        public System.Drawing.Rectangle[] MaskValues
        {
          get { return _maskValues; }
          set { _maskValues = value; }
        }

        //this is for setting the mask out region for 480x360 resolution
        private System.Drawing.Rectangle[] _maskSettingValues = null;

        public System.Drawing.Rectangle[] MaskSettingValues
        {
            get { return _maskSettingValues; }
            set { _maskSettingValues = value; }
        }

        //private System.Windows.Shapes.Polygon[] _maskPolygonValues = null;

        //public System.Windows.Shapes.Polygon[] MaskPolygonValues
        //{
        //    get { return _maskPolygonValues; }
        //    set { _maskPolygonValues = value; }
        //}

        //private System.Windows.Shapes.Polygon[] _maskPolygonSettingValues = null;

        //public System.Windows.Shapes.Polygon[] MaskPolygonSettingValues
        //{
        //    get { return _maskPolygonSettingValues; }
        //    set { _maskPolygonSettingValues = value; }
        //}

        /// <summary>
        /// Blank Constructor
        /// </summary>
        public Camera()
        {
        }

        /// <summary>
        /// Constructor with channel configuration object
        /// </summary>
        /// <param name="_channel"></param>
        public Camera(ProvigilService.ChannelConfiguration _channel)
        {
            _proxyChannel = _channel;
            CameraId = _channel.cameraId;
            Name = _channel.name;
            // CameraType = camera.cameraType;
            SiteName = _channel.siteName;

            SetWidthAndHeight();
            InitMaskValues();
        }

        public Camera(CameraConfigBo camera)
        {
            CameraId =camera.cameraId;
            Name = camera.cameraName;
           // CameraType = camera.cameraType;
            SiteName = camera.siteName;
        }

        public Camera(IvigilJsonList.jsonlist camera)
        {
            CameraId = camera.cameraId;
            Name = camera.cameraName;
        }


        private string name;
        /// <summary>
        /// Gets the camera Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets the camera IP
        /// </summary>
        public string CameraIP
        {
            get { return _proxyChannel.cameraIP ; }
            set { _proxyChannel.cameraIP = value; }
        }

        /// <summary>
        /// Gets the camera Channel Number
        /// </summary>
        public int  ChannelNo
        {
            get { return _proxyChannel.channelNo; }
            set { _proxyChannel.channelNo = value; }
        }            

        /// <summary>
        /// Gets the camera Image Resolution
        /// </summary>
        public string ImageResolution
        {
            get { return _proxyChannel.imageResolution; }
            set {_proxyChannel.imageResolution = value; }
        }

        /// <summary>
        /// To set the width and height of camera based on resolution
        /// </summary>
        private void SetWidthAndHeight()
        {
            if (_proxyChannel.streamResolution != null)
            {
                string resolution = _proxyChannel.streamResolution; //R_WIDTH_BY_Height;
                int startIndex = "R_".Length;
                int endIndex = resolution.LastIndexOf("_BY_");
                Width = Int32.Parse(resolution.Substring(startIndex, endIndex - startIndex));
                Height = Int32.Parse(resolution.Substring(endIndex + "_BY_".Length));
            }
        }

        public int Width 
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        /// <summary>
        /// Gets the camera Image Resolution
        /// </summary>
        public string StreamResolution
        {
            get { return _proxyChannel.streamResolution; }
            set { _proxyChannel.streamResolution = value; }
        }


        /// <summary>
        /// Gets the Feed delay
        /// </summary>
        public int FeedDelay
        {
            get { return _proxyChannel.feedDelay; }
            set { _proxyChannel.feedDelay = value; }
        }


        private string cameraId;
        /// <summary>
        /// Gets camera Id
        /// </summary>
        public string CameraId
        {
            get { return cameraId; }
            set { cameraId= value; }            
        }

        /// <summary>
        /// Initialise Mask values
        /// </summary>
        private void InitMaskValues()
        {
            string maskValues = "";
            //Check if mask values are null
            if (_proxyChannel.maskingList == null)
                return;
            //if mask values are available in the proxy masking list
            if (_proxyChannel.maskingList.Length > 0)
            {
                //Add to mask values
                for (int index = 0; index < _proxyChannel.maskingList.Length; index++)
                {
                    maskValues += _proxyChannel.maskingList[index].x + "," + _proxyChannel.maskingList[index].y + "," + _proxyChannel.maskingList[index].width + "," + _proxyChannel.maskingList[index].height;
                    if (index != _proxyChannel.maskingList.Length - 1)
                        maskValues += ",";
                }
            }
            //setting the mask values
            MaskValues = getMaskRectangle(maskValues);
            InitMaskSettingValues();
        }

        /// <summary>
        /// Get Mask Rectangle
        /// </summary>
        /// <param name="maskValues"></param>
        /// <returns></returns>
        private System.Drawing.Rectangle[] getMaskRectangle(string maskValues)
        {
            //check if the mask values exists
            if (maskValues == null || maskValues == "")
                return null;
            //Add masks values into an array
            string[] maskValueArray = maskValues.Split(',');
            //Check the array length
            if(maskValueArray.Length > 3){

                System.Drawing.Rectangle[] maskRect = new System.Drawing.Rectangle[maskValueArray.Length/4];
                int count = 0;
                //Store the mask values
                for (int index = 0; index < maskValueArray.Length - 1; )
                {
                    //create a rectange
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
                    rect.X = Int32.Parse(maskValueArray[index++]);
                    rect.Y = Int32.Parse(maskValueArray[index++]);
                    rect.Width = Int32.Parse(maskValueArray[index++]);
                    rect.Height = Int32.Parse(maskValueArray[index++]);
                    maskRect[count++] = rect;
                }
                //return masked rectangle
                return maskRect;
            }
            return null;
        }

        /// <summary>
        /// To set the mask out region setting values for 480x360 resolution
        /// </summary>
        private void InitMaskSettingValues()
        {
            if (MaskValues != null && MaskValues.Length > 0)
            {
                MaskSettingValues = new System.Drawing.Rectangle[MaskValues.Length];
                int index = 0;
                foreach (System.Drawing.Rectangle rect in MaskValues)
                {
                    if(Height == 360 && Width == 480)
                        MaskSettingValues[index++] = rect;
                    else
                        MaskSettingValues[index++] = new System.Drawing.Rectangle((int)((rect.X / (Width * 1.0)) * 480), (int)((rect.Y / (Height * 1.0)) * 360), (int)((rect.Width / (Width * 1.0)) * 480), (int)((rect.Height / (Height * 1.0)) * 360));
                }
            }
        }

        public List<System.Drawing.Point[]> GetMaskPolygons()
        {
            try
            {
                MotionMetaData Motiondata = new MotionMetaData();
                string uri = Application.Current.Properties["HostURL"] + "CameraService?event=getMotionAnalytics&cameraid=" + cameraId;
                string buffer = makeWebrequest(uri);
                if (buffer == null)
                    return null;
                //{\"motionAnalytics\":\"\"}
                if (buffer == "{\"motionAnalytics\":\"\"}")
                {
                    if (MaskArea != null && MaskArea.Length > 0)
                    {
                        List<System.Drawing.Point[]> pointList = new List<System.Drawing.Point[]>();
                        string[] maskPolygons = MaskArea.Split(';');
                        foreach (string maskPoly in maskPolygons)
                        {
                            string[] maskPolygonPoints = maskPoly.Split(',');
                            System.Drawing.Point[] pointArray = new System.Drawing.Point[maskPolygonPoints.Length / 2];
                            int count = 0;
                            for (int index = 0; index < maskPolygonPoints.Length - 1; )
                            {
                                System.Drawing.Point p = new System.Drawing.Point();
                                int x = Int32.Parse(maskPolygonPoints[index++]);
                                int y = Int32.Parse(maskPolygonPoints[index++]);
                                p.X = (int)(((x) / 480.0) * (Width));
                                p.Y = (int)(((y) / 360.0) * (Height));
                                pointArray[count] = p;
                                count++;
                            }
                            pointList.Add(pointArray);
                        }
                        return pointList;
                    }
                }
                else
                {
                    Motiondata = JsonConvert.DeserializeObject<MotionMetaData>(buffer);
                    if (!(Motiondata.maskOutList.Count == 0))
                    {   
                               List<System.Drawing.Point[]> pointList = new List<System.Drawing.Point[]>();
                               foreach (var maskoutlist in Motiondata.maskOutList)
                                {       
                                System.Drawing.Point[] pointArray = new System.Drawing.Point[maskoutlist.polygonPoints.Count];
                               
                                for (int index = 0; index <= maskoutlist.polygonPoints.Count-1;index++)
                                {
                                    System.Drawing.Point p = new System.Drawing.Point();
                                    double x = maskoutlist.polygonPoints[index].x;
                                    double y = maskoutlist.polygonPoints[index].y;
                                    p.X = (int)(((x) / 480.0) * (Width));
                                    p.Y = (int)(((y) / 360.0) * (Height));
                                    pointArray[index] = p;
                                   // count++;
                                }
                                pointList.Add(pointArray);
                            }
                            return pointList;
                        }
                    }
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.StackTrace + "\n" + ex.Message);
            }
            return null;
        }

        private string makeWebrequest(string uri)
        {
            try
            {
                WebRequest req = WebRequest.Create(uri);
                req.Proxy = NetworkUtil.GetInstance().SystemProxy;
                WebResponse resp = req.GetResponse();
                Stream st = resp.GetResponseStream();
                StreamReader reader = new StreamReader(st);
                string buffer = null;
                buffer = reader.ReadLine();
                resp.Close();
                return buffer;
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Exception occured while making webrequest" + ex.Message);
                return null;
            }
        }

        public Polygon GetMaskedPolygons(List<I_vigil.MotionAnalytics.BO.Point> listofpoints)
        {
            try
            {
                if (listofpoints == null)
                {
                    return null;
                }

                System.Windows.Shapes.Polygon poly = new System.Windows.Shapes.Polygon();
                for (int i = 0; i < listofpoints.Count; i++)
                {
                    double x = listofpoints[i].x;
                    double y = listofpoints[i].y;
                    poly.Points.Add(new System.Windows.Point(x, y));
                }
                poly.StrokeThickness = 2;
                return poly;

            }
            catch (Exception ex)
            {
                Logger.LogDebug("Exception In  MotionAnalytics_MotionAnalysis --> GetMaskedPolygons.." + ex.Message);
                return null;
            }
        }
        
        /// <summary>
        /// Check if PTZ is Supporting
        /// </summary>
        public bool isSupportPTZ
        { 
            get{ return _proxyChannel.PTZ;}
            set { _proxyChannel.PTZ = value; }
        }


        private string siteName;
        /// <summary>
        /// Get the site name for camera
        /// </summary>
        public string SiteName 
        {
            get { return siteName; }
            set { siteName = value; }
        }

        /// <summary>
        /// Get the camera type(Analog,Axis,Canon,Bosch or Mobotix)
        /// </summary>
        public string CameraType
        {
            get { return _proxyChannel.cameraType; }
            set { _proxyChannel.cameraType = value; }
        }

        /// <summary>
        /// To get the comport number of camera for PTZ operation
        /// </summary>
        public string ComportNo 
        {
            get { return _proxyChannel.comPortNumber; }
            set { _proxyChannel.comPortNumber = value; }
        }

        /// <summary>
        /// To get the camera address to access the ptz
        /// </summary>
        public int Address 
        {
            get { return _proxyChannel.address; }
            set { _proxyChannel.address = value; }
        }

        /// <summary>
        /// To get the camera address to access the ptz
        /// </summary>
        public string CameraVideoType
        {
            get { return _proxyChannel.cameraVideoType ; }
            set { _proxyChannel.cameraVideoType = value; }
        }

        /// <summary>
        /// To get the Sensitivity to access the ptz
        /// </summary>
        public int Sensitivity
        {
            get { return _proxyChannel.senseitivity ; }
            set { _proxyChannel.senseitivity = value; }
        }

        /// <summary>
        /// Return ActiveDays 
        /// </summary>
        public int NoMotionFrames
        {
            get { return _proxyChannel.noMotionFrames; }
            set { _proxyChannel.noMotionFrames = value; }
        }

        /// <summary>
        /// Return NoMotionSeconds 
        /// </summary>
        public int NoMotionSeconds
        {
            get { return _proxyChannel.noMotionSeconds; }
            set { _proxyChannel.noMotionSeconds = value; }
        }

        /// <summary>
        /// Return NoMotionFrames 
        /// </summary>
        public int MotionFrames
        {
            get { return _proxyChannel.motionFrames ; }
            set { _proxyChannel.motionFrames = value; }
        }

        /// <summary>
        /// Return ActiveDays 
        /// </summary>
        public int MotionSeconds
        {
            get { return _proxyChannel.motionSeconds ; }
            set { _proxyChannel.motionSeconds = value; }
        }
        /// <summary>
        /// Camera Decription
        /// </summary>
        public string CameraDesc
        {
            get { return _proxyChannel.description; }
            set { _proxyChannel.description = value; }
        }

        /// <summary>
        /// Compression
        /// </summary>
        public string Compression
        {
            get { return _proxyChannel.compression; }
            set { _proxyChannel.compression = value; }
        }

        /// <summary>
        /// Username
        /// </summary>
        public string Username
        {
            get { return _proxyChannel.username; }
            set { _proxyChannel.username = value; }
        }

        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get { return _proxyChannel.password; }
            set { _proxyChannel.password = value; }
        }

        /// <summary>
        /// EncodeType
        /// </summary>
        public string EncodeType
        {
            get { return _proxyChannel.encoderType; }
            set { _proxyChannel.encoderType = value; }
        }
        
        /// <summary>
        /// Check if PTZ is Supporting
        /// </summary>
        public bool isActive
        {
            get { return _proxyChannel.active; }
            set { _proxyChannel.active = value; }
        }
        /// <summary>
        /// Equals Mehtod
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            //Determines whether the camera Object is equal to the current Object.
            Camera camera = (Camera)obj;
            // returns true if equal
            return this.CameraId.Equals(camera.CameraId);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public string EmailID
        {
            get { return _proxyChannel.email; }
            set { _proxyChannel.email = value; }
        }

        public int MotionDetectionAlgorithm
        {
            get { return _proxyChannel.motionDetectionAlgorithm; }
            set { _proxyChannel.motionDetectionAlgorithm = value; }
        }

        public string MaskArea
        {
            get { return _proxyChannel.maskArea; }
            set { _proxyChannel.maskArea = value; }
        }

        public string Boundary
        {
            get { return _proxyChannel.boundary; }
            set { _proxyChannel.boundary = value; }
        }

        public string TripWire
        {
            get { return tripWire; }
            set { tripWire = value; }
        }

        public string MaskIn
        {
            get { return maskin; }
            set { maskin = value; }
        }
      
    }
}
