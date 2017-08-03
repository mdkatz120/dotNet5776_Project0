using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;


namespace UI
{
    class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (!File.Exists((string)value))
                {
                    throw new Exception("");
                }
                BitmapImage b = new BitmapImage(new Uri((string)value, UriKind.RelativeOrAbsolute));
                Console.WriteLine(b.DpiX);
                return b;
            }
            catch (Exception ex)
            {
                return null;// new BitmapImage();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ((BitmapImage)value).UriSource.AbsolutePath;
            }
            catch
            {
                return "Empty Image";
            }
        }
    }
}
