/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace I_vigil.AppClasses.Converter
{

/*
 * This Class is Used by the weather Control to convert object  
 * 
**/
    //Represents attributes of a value converter to specify the datatypes involved
    [ValueConversion(typeof(string), typeof(string))]
    [ValueConversion(typeof(string), typeof(string))]

    class imageConversion:IValueConverter 
    {
        /// <summary>
        /// Convert Image and applying Custom Logic to a binding.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Assigning Object to string
            string imageName = value.ToString();
            // Getting the URI source 
            Uri uri = new Uri(imageName, UriKind.RelativeOrAbsolute);
            //Used to Construct a Bitmap Frame
            BitmapFrame source = BitmapFrame.Create(uri);
            //return the bitmapframe object
            return source;

        }

        /// <summary>
        /// Function to convert Back and apply custom logic to a binding object.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // nothing to implement
            throw new NotImplementedException();
        }

    }
}
