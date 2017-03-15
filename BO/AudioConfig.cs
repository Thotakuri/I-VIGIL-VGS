/* 
 * Provigil Surveillance Limited 
 */ 


namespace I_vigil.BO
{
    /* 
     * Audio Configuration Class
     * 
     */
    public class AudioConfig
    {
        //Sound Configuration Object
        private ProvigilService.SoundConfig _proxySoundObj = new I_vigil.ProvigilService.SoundConfig();

        /// <summary>
        /// Blank Constructor
        /// </summary>
        public AudioConfig()
        { 
        }

        /// <summary>
        /// Constructor with sound Configuration Object
        /// </summary>
        /// <param name="soundObj"></param>
        public AudioConfig(ProvigilService.SoundConfig soundObj)
        {
            _proxySoundObj = soundObj;
        }

        /// <summary>
        /// Get Configuration file Key
        /// </summary>
        public string Key 
        {
            get { return _proxySoundObj.fileKey; }
            set { _proxySoundObj.fileKey = value; }
        }

        /// <summary>
        /// Get Configuration FilePath
        /// </summary>
        public string FilePath
        {
            get { return _proxySoundObj.filePath;}
            set { _proxySoundObj.filePath = value; }
        }

        public bool ActivateOutputs
        {
            get { return _proxySoundObj.activateOutput; }
            set { _proxySoundObj.activateOutput = value; }
        }

        public long Duration
        {
            get { return _proxySoundObj.duration; }
            set { _proxySoundObj.duration = value; }
        }
    }
}
