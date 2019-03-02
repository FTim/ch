using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChClient.Converters
{
    public class MessageLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "error"||value.ToString()=="fatal") return "red";
            if (value.ToString() == "info") return "orange";
            else return "green";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //nincs rá szükség
            return null;
        }
    }
}
