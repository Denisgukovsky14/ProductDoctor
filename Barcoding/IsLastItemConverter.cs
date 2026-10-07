using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barcoding.Models;

namespace Barcoding
{
    public class IsLastItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Ingridients currentItem && parameter is IList itemsList)
            {
                int index = itemsList.IndexOf(currentItem);
                return index >= 0 && index < itemsList.Count - 1;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
       => throw new NotImplementedException();
    }
}

