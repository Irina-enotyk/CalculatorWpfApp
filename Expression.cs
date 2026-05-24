namespace CalculatorWpfApp
{
    public class Expression
    {
        public string text = string.Empty;

        private double result = 0;

        private char lastSymbol;

        public void Add(char symbol)
        {
            Validate(symbol);
            lastSymbol = symbol;
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
            if (text != string.Empty)
            {
                result = Calculate();
                text = result.ToString();
            }
            return text;
        }

        private bool IsOperator(char symbol)
        {
            return (symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/');
        }

        private void Validate(char symbol)
        {
            if( text == string.Empty)
            {
                if (IsOperator(symbol) && symbol != '-')
                {
                    throw new Exception("Выражение не может начинаться c \n + * /");
                }
            }

            if(text.Length == 1 && text.Last() == '0')
            {
                if (!IsOperator(symbol))
                {
                    throw new Exception("Некорректный ввод!");
                }
            }

            if (IsOperator(lastSymbol))
            {
                if (IsOperator(symbol))
                {
                    throw new Exception("Введите число!");
                }

                if (lastSymbol == '/' && symbol == '0')
                {
                    throw new Exception("На ноль делить нельзя!");
                }
            }
        }

        private double Calculate()
        {
            TextValidation();
            double tempResult = 0;

            if (text != string.Empty)
            {
                var numbers = SeparateDoubles();
                var operators = SeparateOperators();

                tempResult = Multiply(numbers, operators, tempResult);
                tempResult = Divide(numbers, operators, tempResult);
                tempResult = SumAndSubstract(numbers, operators, tempResult);

                result = tempResult;
            }
            return result;
        }

        private void TextValidation()
        {
            if (text[0] == '-')
            {
                text = "0" + text;
            }

            if (IsOperator(lastSymbol))
            {
                throw new Exception("Допишите выражение!");
            }
        }

        private double SumAndSubstract(List<double> numbers, List<char> operators, double tempResult)
        {
            for (int i = 1; i < numbers.Count; i++)
            {
                if (operators[i - 1] == '+')
                {
                    tempResult = numbers[i - 1] + numbers[i];
                }

                if (operators[i - 1] == '-')
                {
                    tempResult = numbers[i - 1] - numbers[i];
                }

                i = UpdateData(i, numbers, operators, tempResult);
            }
            return tempResult;
        }

        private double Divide(List<double> numbers, List<char> operators, double tempResult)
        {
            for (int i = 1; i < numbers.Count; i++)
            {
                if (operators[i - 1] == '/')
                {
                    tempResult = numbers[i - 1] / numbers[i];
                    i = UpdateData(i, numbers, operators, tempResult);
                }
            }
            return tempResult;
        }

        private double Multiply(List<double> numbers, List<char> operators, double tempResult)
        {
            for (int i = 1; i < numbers.Count; i++)
            {
                if (operators[i - 1] == '*')
                {
                    tempResult = numbers[i - 1] * numbers[i];
                    i = UpdateData(i, numbers, operators, tempResult);
                }
            }
            return tempResult;
        }

        private int UpdateData(int i, List<double> numbers, List<char> operators, double tempResult)
        {
            numbers[i - 1] = tempResult;
            numbers.RemoveAt(i);
            operators.RemoveAt(i - 1);
            i--;
            return i;
        }

        private List<double> SeparateDoubles()
        {
            var numbers = new List<double>();
            var parts = text.Split('+', '-', '*', '/');
            for (int i = 0; i < parts.Length; i++)
            {
                var number = Convert.ToDouble(parts[i]);
                numbers.Add(number);
            }
            return numbers;
        }

        private List<char> SeparateOperators()
        {
            var operators = new List<char>();
            foreach (var element in text)
            {
                if (IsOperator(element))
                {
                    operators.Add(element);
                }
            }
            return operators;
        }
    }
}
