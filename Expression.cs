namespace CalculatorWpfApp
{
    public class Expression
    {
        private string text = string.Empty;
        private List<double> operands = new List<double>();
        private List<char> operators = new List<char>();
        private double result = 0;
        private double tempResult = 0;
        private char lastInputSymbol;

        public void Add(char symbol)
        {
            if (lastInputSymbol == '+' || lastInputSymbol == '-' || lastInputSymbol == '*' || lastInputSymbol == '/')
            {
                if (symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/')
                {
                    return;
                }
            }
            lastInputSymbol = symbol;
            text += symbol;
        }

        public string GetText()
        {
            return text;
        }

        public void Clear()
        {
            text = string.Empty;
        }

        public string GetResult()
        {
            Calculate();

            operands.Clear();
            operators.Clear();

            text = result.ToString();
            return text;
        }

        private void Calculate ()
        {
            TextValidation();
            tempResult = 0;

            if (text != string.Empty)
            {
                SeparateOperands();
                SeparateOperators();

                if (operands.Count < 2)
                {
                    return;
                }

                DoMultiply();
                DoDivide();
                DoSumAndSubstract();

                result = tempResult;
            }
        }

        private void TextValidation()
        {
            if (text[0] == '-')
            {
                text = "0" + text;
            }
        }

        private void DoSumAndSubstract()
        {
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
                i = UpdateData(i);
            }
        }

        private void DoDivide()
        {
            for (int i = 1; i < operands.Count; i++)
            {
                if (operators[i - 1] == '/')
                {
                    tempResult = operands[i - 1] / operands[i];
                    i = UpdateData(i);
                }
            }
        }

        private void DoMultiply()
        {
            for (int i = 1; i < operands.Count; i++)
            {
                if (operators[i - 1] == '*')
                {
                    tempResult = operands[i - 1] * operands[i];
                    i = UpdateData(i);
                }
            }
        }

        private int UpdateData(int i)
        {
            operands[i - 1] = tempResult;
            operands.RemoveAt(i);
            operators.RemoveAt(i - 1);
            i--; 
            return i;
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
