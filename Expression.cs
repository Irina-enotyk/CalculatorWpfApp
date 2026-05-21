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
        private bool lastSymbolIsNumber;
        private string errorMessage = string.Empty;

        public void Add(char symbol)
        {
            errorMessage = string.Empty;
            InputValidator(symbol);
            if(errorMessage == string.Empty)
            {
                lastInputSymbol = symbol;
                text += symbol;
            }
        }

        private bool IsOperator(char symbol)
        {
            return (symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/');
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
            errorMessage = string.Empty;
            if (text != string.Empty)
            {
                Calculate();

                if (errorMessage == string.Empty)
                {
                    operands.Clear();
                    operators.Clear();

                    text = result.ToString();
                }
            }
            return text;
        }

        public string GetErrorMessage()
        {
            return errorMessage;
        }

        private void InputValidator(char symbol)
        {
            //Какая-то тавтология получается: IsOperator и lastSymbolIsNumber

            if (!IsOperator(symbol))
            {
                lastSymbolIsNumber = true;

                errorMessage = string.Empty;
            }

            if (IsOperator(lastInputSymbol))
            {
                if (IsOperator(symbol))
                {
                    lastSymbolIsNumber = false;
                    errorMessage = "Введите число!";
                }

                if (symbol == '0')
                {
                    errorMessage = "На ноль делить нельзя!";
                }
            }
        }

        private void Calculate ()
        {
            TextValidation();
            tempResult = 0;

            if (text != string.Empty)
            {
                if (!lastSymbolIsNumber)
                {
                    errorMessage = "Проверьте выражение!";
                    return;
                }

                if (text[0] == 0 && lastSymbolIsNumber)
                {
                    errorMessage = "Проверьте выражение!";
                    return;
                }

                SeparateOperands();
                SeparateOperators();
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

            if (text[0] == 0 && (text[1] < 48 || text[1] > 57))
            {
                text = text.Remove(0, 1);
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
