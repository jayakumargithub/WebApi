using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Globalization;
using WebApi.Models;

namespace WebApi.ModelBinder
{
    /// <summary>
    /// http://localhost:62870/api/color/?color=10,10,10
    /// </summary>
    public class ColorTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {

            if (value is string)
            {
                var rgbCodes = ((string)value).Split(',');

                if (rgbCodes.Length == 3)
                {
                    double red = 0.0;
                    double green = 0.0;
                    double blue = 0.0;
                    if (Double.TryParse(rgbCodes[0], out red) && Double.TryParse(rgbCodes[1], out green) && Double.TryParse(rgbCodes[2], out blue)){
                        return new Color() { Red = red, Green = green, Blue = blue };
                    }
                } 

            }
            if (context != null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            else
            {
                return null;
            }
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return  ((Color)value).Red + "," + ((Color)value).Green + "," + ((Color)value).Blue;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}