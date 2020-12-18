using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HumanResources.Converters

{
    #region "Opis"

    /* Chcemy na podstawie wartości true lub false (bool) wyświetlić odpowiedni kolor,
     * dlatego najpierw musimy sobie zdefiniować nowy konwerter.
     * Tam możemy ustawić, że np chcemy żeby wartość true zwracała nam kolor czerwony, a wartość false żółty.
     * Jeżeli będziesz robił konwerter na podstawie wynagrodzenia, to możesz zdefiniować tam,
     * że np powyżej 10000 jest kolor czerwony, poniżej 10000 np żółty, powyżej 20000 jeszcze jakiś inny,
     * tutaj masz pełną dowolność. I taki konwerter może wyglądać w ten sposób:
     * Ważne jest to, że klasa konwertera musi implementować interfejs IValueConverter,
     * a co za tym idzie 2 metody, ale akurat w naszym przypadku będziemy potrzebować tylko 1 o nazwie Convert.
     * I w tej metodzie będzie logika odpowiedzialna za konwertowanie, danych wejściowych,
     * czyli jeżeli bindujemy z bool'em to wtedy obsługujemy bool'a tak jak na przykładzie.
     * Jeżeli bindujemy np decimal, to sobie skonwerujemy object do decimal i następnie wykonamy odpowiednią logikę.
     * 
     * Następnie w klasie .xaml musisz zdefiniować, swój konwerter, tak aby był on widoczny w klasie,
     * której będziemy go używać (możesz też zdefiniować go globalnie).
     * Najpierw dodajemy namespace na górze:
     * xmlns:converters="clr-namespace:HumanResources.Converters"
     * Musisz mieć ustawioną odpowiednią ścieżkę.
     * Następnie dodajesz nowe zasoby po zamknięciu pierwszego tagu mah:MetroWindow.Resources
     * <mah:MetroWindow.Resources>
     *    <converters:BoolToBackgroundColor x:Key="boolToBackgroundColor" />
     * </mah:MetroWindow.Resources>
     * Dzięki temu będziemy mogli użyć naszego konwertera w tej klasie.
     * 3) Następnie dodajemy style dla danej kolumny i mówimy, że na podstawie właściwości Released (bool), chcemy wyświetlić odpowiedni kolor tła komórki:
     *   <DataGridCheckBoxColumn Binding="{Binding Released}" Header="Zwolniony">
     *               <DataGridCheckBoxColumn.CellStyle>
     *                   <Style TargetType="DataGridCell">
     *                       <Setter Property="Background" Value="{Binding Released, Converter={StaticResource boolToBackgroundColor}}" />
     *                   </Style>
     *               </DataGridCheckBoxColumn.CellStyle>
     *           </DataGridCheckBoxColumn>
     * 
     */
    #endregion

    public class BoolToBackgroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToBoolean(value))
            {
                return Brushes.AntiqueWhite;
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class SalaryToBackgroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToDecimal(value) > 5000)
            {
                return Brushes.Red;
                
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ValueToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int input;
            try
            {
                DataGridCell dgc = (DataGridCell)value;
                System.Data.DataRowView rowView = (System.Data.DataRowView)dgc.DataContext;
                input = (int)rowView.Row.ItemArray[dgc.Column.DisplayIndex];
            }
            catch (InvalidCastException e)
            {
                return DependencyProperty.UnsetValue;
            }
            switch (input)
            {
                case 1: return Brushes.Red;
                case 2: return Brushes.White;
                case 3: return Brushes.Blue;
                default: return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }


}
