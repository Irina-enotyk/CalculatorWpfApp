using System.Windows;
using System.Windows.Controls;

namespace CalculatorWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string expression = string.Empty;
        private double result;

        public MainWindow()
        {
            InitializeComponent();

            //Не знаю, так нормально? Наверное, можно проще как-то.
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
            Clear();
        }

        private void Clear()
        {
            expression = string.Empty;
            displayLabel.Content = expression;
        }

        //Этот метод очень сырой, знаю.
        //Думаю, надо создать класс Expression и методы работы с ним.
        //Плюс добавить проверки на корректность введенного выражения. 
        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            var numbers = expression.Split('+', '-', '*', '/');
            var symbols = new List<char>();

            foreach (var symbol in expression)
            {
                if (symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/')
                {
                    symbols.Add(Convert.ToChar(symbol));
                }
            }
            
            for (int i = 1; i <  numbers.Length; i += 1)
            {
                if (i == 1)
                {
                    result = Convert.ToDouble(numbers[i - 1]);
                }

                var operand = Convert.ToDouble(numbers[i]);

                if (symbols[i - 1] == '+')
                {
                    result +=  operand;    
                }
                else if (symbols[i - 1] == '-')
                {
                    result -= operand;
                }
                else if (symbols[i - 1] == '*')
                {
                    result *= operand;
                }
                else if (symbols[i - 1] == '/')
                {
                    if (operand == 0)
                    {
                        MessageBox.Show("На ноль делить нельзя");
                        Clear();
                        return;
                    }
                    else
                    {
                        result /= operand;
                    }
                }
            }
            
            expression = result.ToString();
            displayLabel.Content = expression;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentButton = sender as Button;
            expression += currentButton.Content.ToString();
            displayLabel.Content = expression;
        }
    }
}