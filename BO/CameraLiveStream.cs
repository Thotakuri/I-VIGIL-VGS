/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Interop;
using I_vigil.Util;
using I_vigil.Video;

namespace I_vigil.BO
{
    public class CameraLiveStream
    {

        //Live Url
        private string liveUrl = null;
        //camera id
        private string _cameraId = null;
        //directory Name
        private string directoryName;
        //Session ID
        private string _sessionId = null;
        //Video Decoder
        private IDecoder decoder;
        // Bitmap Buffer 
        private InteropBitmap _bitmapImage = null;
        //host Url
        private string _hostURL = null;
        //width
        private int _imageWidth = 320;
        //motion changed Time stamp
        private long MotionChangedTimeStamp = 0;
        //back up bitmap image
        private Bitmap backupImage = null;
        //original Image bitmap
        private Bitmap originalImage = null;
        //buffer pointer
        private IntPtr _bufferPointer = IntPtr.Zero;
        //Copy Memory
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);

        //height
        private int _imageHeight = 240;
        //live thread
        private Thread liveThread;
        //stop live signal
        private ManualResetEvent stopLiveSignal;

        //show mask area control variable
        private bool _showMaskArea = false;
        //Masked Rectangle
        System.Drawing.Rectangle[] _maskRect = null;
        // Delegate for update image UI handler
        public delegate void UpdateImageUIHandler();
        //delegate for showing disconnected image
        public delegate void ShowDisconnected(bool enable);
        public int ImageWidth
        {
            get { return _imageWidth; }
            set { _imageWidth = value; }
        }
        

        public System.Drawing.Rectangle[] MaskRect
        {
            get { return _maskRect; }
            set { _maskRect = value; }
        }

        public bool ShowMaskArea
        {
            get { return _showMaskArea; }
            set { _showMaskArea = value; }
        }

        public int ImageHeight
        {
            get { return _imageHeight; }
            set { _imageHeight = value; }
        }
        
        public string HostURL
        {
            get { return _hostURL; }
            set { _hostURL = value; }
        }


        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }
        

        public CameraLiveStream()
        {
        }
        
        /// <summary>
        /// Start capturing 
        /// </summary>
        public void StartCapturing()
        {
            //initialize decoder
            InitDecoder();

            //Coining the Live url
            //liveUrl = HostURL + "/SnapShotMC?channel=" + _cameraId + "&station=recordingServer1&timeStamp=";

            // Add session id to the url
            liveUrl = HostURL + "/VideoStream?channel=" + _cameraId + "&station=recordingServer1&sessionId=" + SessionId + "&timeStamp=";
            MotionChangedTimeStamp = 0;
            //start live thread
            StartLiveThread();
        }

        /// <summary>
        /// Initialize Video decoder
        /// </summary>
        private void InitDecoder()
        {
            //initialize decoder
            if (decoder != null)
            {
                decoder.Close();
                decoder = null;
            }
            decoder = DecoderFactory.CreateDecoder(ImageWidth, ImageHeight);
            decoder.Init();
        }
        /// <summary>
        /// Start Live thread
        /// </summary>
        private void StartLiveThread()
        {
            if (liveThread == null)
            {
                stopLiveSignal = new ManualResetEvent(false);
                liveThread = new Thread(ShowLive);
                liveThread.Start();
            }
        }

        /// <summary>
        /// Show Live 
        /// </summary>
        public void ShowLive()
        {
            //Stop Live Signal 
            while (!stopLiveSignal.WaitOne(0, true))
            {
                try
                {
                    //directory name 
                    directoryName = liveUrl + MotionChangedTimeStamp;
                    //Get Image Map 
                    GetLiveImage(directoryName);
                    Thread.Sleep(10);
                }
                catch (ThreadAbortException tae) { }
                catch (ThreadInterruptedException tie) { }
                catch (Exception ex)
                {
                    Logger.LogDebug(ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// Get Image from URI
        /// </summary>
        /// <param name="uri"></param>
        public void GetLiveImage(string uri)
        {
            try
            {
                //Create Web request of time out is 10000
                WebRequest req = WebRequest.Create(uri);
                req.Proxy = NetworkUtil.GetInstance().SystemProxy;
                req.Timeout = 100000;
                //get response 
                WebResponse resp = req.GetResponse();
                //New buffer to 25000
                byte[] tmpbuf = new byte[1024];
                MemoryStream imgstream = new MemoryStream();
                //returns the data stream
                Stream st = resp.GetResponseStream();
                int read;
                int offset = 0;

                while ((read = st.Read(tmpbuf, 0, 1024)) > 0)
                {
                    imgstream.Write(tmpbuf, 0, read);
                    offset += read;
                }
                //Close the response
                resp.Close();
                //if Offset is zero
                if (offset == 0)
                {
                    //get the backup Image
                    if (originalImage != null)
                    {
                        backupImage = originalImage;
                        //update to the original Image
                        UpdateImageUI();
                    }
                    // disconnected image dispatcher
                    //ShowDisconnected upUI = new ShowDisconnected(EnableDisconnectedImage);
                    //this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, upUI, true);

                    //if camera is disconnected then try after 5 seconds.
                    Thread.Sleep(5000);
                    return;
                }
                byte[] buf = imgstream.ToArray();
                imgstream.Close(); imgstream = null;
                if (buf[0] == (byte)0xE0)
                {
                    // if response is timestamp then set the time stamp to previous image
                    string timestamp = Encoding.ASCII.GetString(buf, 1, offset - 1);
                    //Update Time Stamp from the buffer
                    UpdateTimeStamp(timestamp);
                }
                else if (buf[0] == (byte)0x1C)
                {
                    //Debug.WriteLine(offset.ToString());
                    //if response is image buffer then display the image
                    //and set into backup image
                    int index;
                    MotionChangedTimeStamp = GetTimeStamp(buf, out index);

                    byte imgType = buf[index++];

                    //image memory Stream
                    MemoryStream imageMemoryStream = new MemoryStream();
                    imageMemoryStream.Write(buf, index, offset - index);
                    imageMemoryStream.Seek(0, SeekOrigin.Begin);
                    if (backupImage != null)
                        backupImage.Dispose(); // Dispose the backup image
                    if (originalImage != null)
                        originalImage.Dispose(); //Dispose the original Image

                    if (imgType == 1)
                    {
                        backupImage = decoder.DecodeAsBitmap(imageMemoryStream.ToArray());
                    }
                    else
                    {
                        backupImage = new Bitmap(imageMemoryStream);
                    }

                    imageMemoryStream.Close();
                    //show mask area and mask rectangle
                    if (ShowMaskArea && MaskRect != null && MaskRect.Length > 0)
                        DrawMaskArea();
                    //update UI Image
                    UpdateImageUI();
                }
            }
            catch (ThreadAbortException tae) { }
            catch (ThreadInterruptedException tie) { }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.StackTrace);
            }
            Thread.Sleep(10);
        }

        /// <summary>
        /// Draw Mask Area
        /// </summary>
        private void DrawMaskArea()
        {
            try
            {
                //Graphics Image 
                Graphics graphicsImage = Graphics.FromImage(backupImage);
                //Red Pen 
                System.Drawing.Pen redPen = new System.Drawing.Pen(System.Drawing.Color.Red);
                //canvas rectangle for the mask Rectangle
                foreach (System.Drawing.Rectangle canvasRectangle in MaskRect)
                    graphicsImage.DrawRectangle(redPen, canvasRectangle);
            }
            catch (ThreadAbortException tae) { }
            catch (ThreadInterruptedException tie) { }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.StackTrace);
            }
        }

        /// <summary>
        /// Get Time Stamp
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private long GetTimeStamp(byte[] buf, out int index)
        {
            long TimeStamp = 0;
            index = 0;
            try
            {
                while (buf[index] != ((byte)0xE0))
                    index++;
                // Get Time STAMP
                byte[] TimeBuf = new byte[index - 1];
                Array.Copy(buf, 1, TimeBuf, 0, index - 1);
                index++;
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                //get the enclosing
                string timeStamp = encoding.GetString(TimeBuf);
                //Convert string to integer
                TimeStamp = Int64.Parse(timeStamp);

            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.StackTrace);
            }
            return TimeStamp;
        }

        /// <summary>
        /// Update Time Stamp
        /// </summary>
        /// <param name="timeStamp"></param>
        public void UpdateTimeStamp(string timeStamp)
        {
            try
            {
                //Draw Time Stamp in the top
                DrawTimeStamp(timeStamp);
                if (ShowMaskArea && MaskRect != null && MaskRect.Length > 0)
                    DrawMaskArea();
                //Update Image 
                UpdateImageUI();
            }
            catch (ThreadAbortException tae) { }
            catch (ThreadInterruptedException tie) { }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.StackTrace);
            }
        }

        /// <summary>
        /// Draw Time Stamp
        /// </summary>
        /// <param name="timeStamp"></param>
        private void DrawTimeStamp(string timeStamp)
        {
            try
            {
                if (backupImage == null) return;

                Font fontFamily = new Font("Arial", TimeStampUtil.GetFontSize(ImageWidth, ImageHeight));

                Graphics graphicsImage = Graphics.FromImage(backupImage);
                SizeF fontMatrix = graphicsImage.MeasureString(timeStamp, fontFamily);
                SolidBrush whiteBrush = new SolidBrush(System.Drawing.Color.Blue);
                SolidBrush blackBrush = new SolidBrush(System.Drawing.Color.White);

                if (ImageWidth == 480)
                {
                    Rectangle canvas = new Rectangle(0, 0, (int)fontMatrix.Width - 4, (int)fontMatrix.Height + 3);
                    graphicsImage.FillRectangle(whiteBrush, canvas);
                    graphicsImage.DrawString(timeStamp, fontFamily, blackBrush, new System.Drawing.Rectangle(-4, 0, (int)fontMatrix.Width + 10, (int)fontMatrix.Height));
                }
                else if (ImageHeight == 480)
                {
                    Rectangle canvas = new Rectangle(0, 0, (int)fontMatrix.Width - 18, (int)fontMatrix.Height + 3);
                    graphicsImage.FillRectangle(whiteBrush, canvas);
                    graphicsImage.DrawString(timeStamp, fontFamily, blackBrush, new System.Drawing.Rectangle(-4, 0, (int)fontMatrix.Width + 10, (int)fontMatrix.Height));
                }
                else if (ImageWidth == 352)
                {
                    Rectangle canvas = new Rectangle(0, 0, (int)fontMatrix.Width - 10, (int)fontMatrix.Height + 2);
                    graphicsImage.FillRectangle(whiteBrush, canvas);
                    graphicsImage.DrawString(timeStamp, fontFamily, blackBrush, new System.Drawing.Rectangle(-2, 0, (int)fontMatrix.Width + 10, (int)fontMatrix.Height));
                }
                else if (ImageWidth == 800)
                {
                    Rectangle canvas = new Rectangle(0, 0, (int)fontMatrix.Width - 10, (int)fontMatrix.Height + 2);
                    graphicsImage.FillRectangle(whiteBrush, canvas);
                    graphicsImage.DrawString(timeStamp, fontFamily, blackBrush, new System.Drawing.Rectangle(-5, -1, (int)fontMatrix.Width + 10, (int)fontMatrix.Height));
                }
                else
                {
                    Rectangle canvas = new Rectangle(0, 0, (int)fontMatrix.Width - 6, (int)fontMatrix.Height + 2);
                    graphicsImage.FillRectangle(whiteBrush, canvas);
                    graphicsImage.DrawString(timeStamp, fontFamily, blackBrush, new System.Drawing.Rectangle(-2, -1, (int)fontMatrix.Width + 10, (int)fontMatrix.Height));
                }
            }
            catch (ThreadAbortException tae) { }
            catch (ThreadInterruptedException tie) { }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.StackTrace);
            }
        }

        /// <summary>
        /// Enable Disconnected Image
        /// Making visibility hidden or visible
        /// </summary>
        /// <param name="enable"></param>
        public void EnableDisconnectedImage(bool enable)
        {
            //disconnectedControl.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Update the Image
        /// </summary>
        public void UpdateImageUI()
        {
            try
            {
                if (backupImage == null) return;

                //Create a rectangle
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, backupImage.Width, backupImage.Height);
                //Get bitmap data
                BitmapData bitmapData = backupImage.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                if (_bufferPointer != IntPtr.Zero)
                {
                    CopyMemory(_bufferPointer, bitmapData.Scan0, bitmapData.Stride * backupImage.Height);
                }
                //Unlock Bits from the memory
                backupImage.UnlockBits(bitmapData);

                //add a dispatcher for invalidating Image
                //UpdateImageUIHandler upUI = new UpdateImageUIHandler(InvalidateImage);
                //this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, upUI);
            }
            catch (ThreadAbortException tae) { }
            catch (ThreadInterruptedException tie) { }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.StackTrace);
            }
        }

        /// <summary>
        /// Invalidate Image 
        /// </summary>
        public void InvalidateImage()
        {
            _bitmapImage.Invalidate();
            EnableDisconnectedImage(false);

        }


    }
}
