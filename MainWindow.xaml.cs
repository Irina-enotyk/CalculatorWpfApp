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

        public MainWindow()
        {
            InitializeComponent();
            CreateButtonsSubsctibtion();
        }

        private void CreateButtonsSubsctibtion()
        {
            Button[] buttons =
                [zeroButton,
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
                devideButton];

            foreach (var button in buttons)
            {
                button.Click += Button_Click;
            }

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
            try
            {
                var result = expression.GetResult();
                displayLabel.Content = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentButton = sender as Button;
            var symbol = Convert.ToChar(currentButton.Content);

            try
            {
                expression.Add(symbol);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            displayLabel.Content = expression.GetText();
        }
    }
}
