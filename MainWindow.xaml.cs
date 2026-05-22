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
            CreateButtonsSubsctibtion();
        }

        private void CreateButtonsSubsctibtion()
        {
            Button[] buttons =
                [
                zeroButton,
                oneButton,
                twoButton,
                threeButton,
                fourButton,
                fiveButton,
                sixButton,
                sevenButton,
                eightButton,
                nineButton,
                plusButton,
                minusButton,
                multiplyButton,
                devideButton,
                resultButton,
                clearButton
                ];

            foreach (var button in buttons)
            {
                button.Click += Button_Click;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            expression.Clear();
            displayLabel.Content = expression.GetText();
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            var result = expression.GetResult();

            UpdateMessage();
            if (message != string.Empty)
            {
                ShowMessage();
                return;
            }
            displayLabel.Content = result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentButton = sender as Button;

            var symbol = Convert.ToChar(currentButton.Content);
            expression.Add(symbol);

            UpdateMessage();
            if (message != string.Empty)
            {
                ShowMessage();
                return;
            }

            displayLabel.Content = expression.GetText();
        }

        private void UpdateMessage()
        {
            message = string.Empty;
            message = expression.GetErrorMessage();
        }

        private void ShowMessage()
        {
            if (message != string.Empty)
            {
                MessageBox.Show(message);
            }
        }
    }
}