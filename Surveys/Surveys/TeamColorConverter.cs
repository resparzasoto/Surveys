using System;
using System.Globalization;
using Xamarin.Forms;

namespace Surveys.Core
{
    public class TeamColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var team = (string)value;
            Color colorPlayera = Color.Transparent;

            switch (team)
            {
                case "Ámerica":
                case "Peñarol":
                    colorPlayera = Color.Yellow;
                    break;
                case "Boca Juniors":
                case "Alianza Lima":
                case "Colo-Colo":
                    colorPlayera = Color.Blue;
                    break;
                case "Caracas FC":
                case "Saprissa":
                    colorPlayera = Color.Purple;
                    break;
                case "Real Madrid":
                    colorPlayera = Color.Fuchsia;
                    break;
                default:
                    break;
            }

            return colorPlayera;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
