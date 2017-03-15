/* 
 * Provigil Surveillance Limited 
 */ 


namespace I_vigil.BO
{
    /*
     * Weather Class
     */
    public class Weather
    {
        //Title For the weather Image
        private string _title;
        //weather Condition 
        private string _condition;
        //temperature 
        private string _temp;
        //Unit of temperature
        private string _unit;
        //gets the Url
        private string _url;
        //Sunrise status
        private string _sunrise;
        //Sunset Status
        private string _sunset;
        //High temperature
        private string _high;
        //Low temperature
        private string _low;
        //Day or Night
        private bool _day;

        /// <summary>
        /// Gets and sets Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        
        /// <summary>
        /// Gets and sets Condition
        /// </summary>
        public string Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }
        
        /// <summary>
        /// Gets and sets Temperature
        /// </summary>
        public string Temp
        {
            get { return _temp; }
            set { _temp = value; }
        }
        
        /// <summary>
        /// Gets and sets Temperature Unit
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        
        /// <summary>
        /// Gets and sets Image Url
        /// </summary>
        public string ImgUrl
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// Gets and sets Sunrise
        /// </summary>
        public string Sunrise
        {
            get { return _sunrise; }
            set { _sunrise = value; }
        }
       
        /// <summary>
        /// gets and sets the sunset
        /// </summary>
        public string Sunset
        {
            get { return _sunset; }
            set { _sunset = value; }
        }
               
        /// <summary>
        /// Gets and sets high temperature
        /// </summary>
        public string High
        {
            get { return _high; }
            set { _high = value; }
        }

       /// <summary>
       /// Gets and sets low temperature
       /// </summary>
        public string Low
        {
            get { return _low; }
            set { _low = value; }
        }
               
        /// <summary>
        /// Sets and gets the day
        /// </summary>
        public bool Day
        {
            get { return _day; }
            set { _day = value; }
        }
    }
}
