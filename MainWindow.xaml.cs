using System.Windows;
using System.Windows.Controls;

namespace CalculatorWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Expression expression = new Expression();
        private string message = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            //Не знаю, так нормально? Наверное, можно проще как-то.
            //Может, как-то пройтись циклом по всем кнопкам формы и для каждой создать подписку?
            zeroButton.Click += Button_Click;
            oneButton.Click += Button_Click;
            twoButton.Click += Button_Click;
            threeButton.Click += Button_Click;
            fourButton.Click += Button_Click;
            fiveButton.Click += Button_Click;
            sixButton.Click += Button_Click;
            sevenButton.Click += Button_Click;
            eightButton.Click += Button_Click;
            nineButton.Click += Button_Click;
            plusButton.Click += Button_Click;
            minusButton.Click += Button_Click;
            multiplyButton.Click += Button_Click;
            devideButton.Click += Button_Click;
            resultButton.Click += ResultButton_Click;
            clearButton.Click += ClearButton_Click;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            expression.Clear();
            displayLabel.Content = expression.GetText();
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateMessage();
            var result = expression.GetResult(out message);

            ShowMessage();
            displayLabel.Content = result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentButton = sender as Button;

            var symbol = Convert.ToChar(currentButton.Content);
            UpdateMessage();

            expression.Add(symbol, out message);

            ShowMessage();

            displayLabel.Content = expression.GetText();
        }

        private void ShowMessage()
        {
            if (message != string.Empty)
            {
                MessageBox.Show(message);
            }
        }

        private void UpdateMessage()
        {
            message = string.Empty;
        }
    }
}