namespace CalculatorWpfApp
{
    public class Expression
    {
        private string text = string.Empty;
        private List<double> operands = new List<double>();
        private List<char> operators = new List<char>();
        private double result = 0;

        public void Add(string symbol)
        {
            text += symbol;
        }

        public void Clear()
        {
            text = string.Empty;
        }

        public double GetResult()
        {
            Calculate();
            return result;
        }

        private void Calculate ()
        {
            double tempResult = 0;

            if (text != string.Empty)
            {
                SeparateOperands();
                SeparateOperators();

                if( operands.Count < 2 || operators.Count == 0 ) 
                {
                    return;
                }

                for (int i = 1; i < operands.Count; i++)
                {
                    if (operators[i - 1] == '*')
                    {
                        tempResult = operands[i - 1] * operands[i];
                    }

                    if (operators[i - 1] == '/')
                    {
                        tempResult = operands[i - 1] / operands[i];
                    }

                    operands[i - 1] = tempResult;
                    operands.RemoveAt(i);
                    operators.RemoveAt(i - 1);
                    i--;
                }

                for (int i = 1; i < operands.Count; i++)
                {
                    if (operators[i - 1] == '+')
                    {
                        tempResult = operands[i - 1] + operands[i];
                    }

                    if (operators[i - 1] == '-')
                    {
                        tempResult = operands[i - 1] - operands[i];
                    }

                    operands[i - 1] = tempResult;
                    operands.RemoveAt(i);
                    operators.RemoveAt(i - 1);
                    i--;
                }
                result = tempResult;
            }
        }

        private void SeparateOperands()
        {
            var numbers = text.Split('+', '-', '*', '/');
            for (int i = 0; i < numbers.Length; i++)
            {
                var operand = Convert.ToDouble(numbers[i]);
                operands.Add(operand);
            }
        }

        private void SeparateOperators()
        {
            foreach (var element in text)
            {
                if (element == '+' || element == '-' || element == '*' || element == '/')
                {
                    operators.Add(element);
                }
            }
        }
    }
}
