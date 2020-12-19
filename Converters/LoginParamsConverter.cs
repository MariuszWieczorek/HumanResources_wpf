using HumanResources.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace HumanResources.Converters
{
    public class LoginParamsConverter : IMultiValueConverter
    {
        #region "Opis"
        /*
         * Tym razem to jest konwerter, do którego możemy przekazać wiele parametrów z commandparameter i zamieni nam to 
         * na 1 naszą wcześniej przygotowaną klasę, LoginParams
         */

        #endregion

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new LoginParams { Window = values[0] as MetroWindow, PasswordBox = values[1] as PasswordBox };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
