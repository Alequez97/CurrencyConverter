using CryptoCurrencyConverterClassLibrarry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoCurrencyConverterUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private UiDataProvider _uiDataProvider;
        private FiatCurrencyModel fiatCurrencyModel;

        public MainWindow()
        {
            InitializeComponent();
            FillComboBoxes();

            _uiDataProvider = new UiDataProvider();
        }

        private void FillComboBoxes()
        {

            var dataSource = Enum.GetValues(typeof(FiatCurrencyEnum)).Cast<FiatCurrencyEnum>();

            fiatCurrancyFromComboBox.ItemsSource = dataSource;
            fiatCurrancyToComboBox.ItemsSource = dataSource;

            fiatCurrancyFromComboBox.SelectedIndex = 0;
            fiatCurrancyToComboBox.SelectedIndex = 1;
        }

        private void fiatCurrancyConvertButton_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorFields();
            if (ValidateInput() == false) return;

            FiatCurrencyEnum fromEnum = (FiatCurrencyEnum)fiatCurrancyFromComboBox.SelectedIndex;
            FiatCurrencyEnum toEnum = (FiatCurrencyEnum)fiatCurrancyToComboBox.SelectedIndex;
            var inputText = fiatCurrancyTextBox.Text;

            if (fromEnum.ToString().Equals(toEnum.ToString()))
            {
                fiatCurrancyResultTextBlock.Text = $"{inputText} {fromEnum} = {inputText:N} {toEnum}";
            }
            else
            {
                Task.Run(() =>
                {
                    fiatCurrencyModel = _uiDataProvider.RecieveDataAboutFiatCurreny(fromEnum);
                }).Wait();

                var userInputAmount = double.Parse(inputText, CultureInfo.InvariantCulture);
                var convertionResult = ConvertCurrency(fromEnum, userInputAmount, toEnum);
                fiatCurrancyResultTextBlock.Text = $"{userInputAmount:N} {fromEnum} = {convertionResult:N} {toEnum}";
            }
        }

        private double ConvertCurrency(FiatCurrencyEnum from, double userInputAmount, FiatCurrencyEnum to)
        {
            return to switch
            {
                FiatCurrencyEnum.EUR => userInputAmount * double.Parse(fiatCurrencyModel.Rates.EUR, CultureInfo.InvariantCulture),
                FiatCurrencyEnum.USD => userInputAmount * double.Parse(fiatCurrencyModel.Rates.USD, CultureInfo.InvariantCulture),
                FiatCurrencyEnum.GBP => userInputAmount * double.Parse(fiatCurrencyModel.Rates.GBP, CultureInfo.InvariantCulture),
                _ => 1.0d,
            };
        }

        private bool ValidateInput()
        {
            var inputText = fiatCurrancyTextBox.Text;
            if (inputText.Trim().Equals(""))
            {
                fiatCurrencyErrorLabel.Content = "Error. Please input value";
                fiatCurrancyTextBox.BorderBrush = Brushes.Red;
                return false;
            }
            try
            {
                Convert.ToDouble(inputText);
            }
            catch 
            {
                fiatCurrencyErrorLabel.Content = "Error. Please input correct number";
                fiatCurrancyTextBox.BorderBrush = Brushes.Red;
                return false;
            }

            return true;
        }

        private void ClearErrorFields()
        {
            fiatCurrencyErrorLabel.Content = "";
            fiatCurrancyTextBox.BorderBrush = Brushes.LightGray;
        }

        private void Change_Currencies_Button_Click(object sender, RoutedEventArgs e)
        {
            int fromIndex = fiatCurrancyFromComboBox.SelectedIndex;
            int toIndex = fiatCurrancyToComboBox.SelectedIndex;

            fiatCurrancyFromComboBox.SelectedIndex = toIndex;
            fiatCurrancyToComboBox.SelectedIndex = fromIndex;
        }
    }
}
