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
            var result = expression.GetResult();
            displayLabel.Content = result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentButton = sender as Button;
            expression.Add(Convert.ToChar(currentButton.Content));
            displayLabel.Content = expression.GetText();
        }
    }
}