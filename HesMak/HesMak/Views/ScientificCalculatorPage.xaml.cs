using Microsoft.Maui.Controls;
using System;
using System.Numerics; 

namespace HesMak.Views
{
    public partial class ScientificCalculatorPage : ContentPage
    {
        private double _scientificLastValue = 0;
        private string _scientificOperator = null;
        private bool _scientificIsNewEntry = true;

        public ScientificCalculatorPage()
        {
            InitializeComponent();
        }

      
        private void OnScientificButtonClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            string btnText = btn.Text;

            switch (btnText)
            {
                case "C":
                    ResetCalculator();
                    break;

                case "⌫":
                    Backspace();
                    break;

                case "π":
                    DisplayFormattedNumber(Math.PI);
                    break;

                case "e":
                    DisplayFormattedNumber(Math.E);
                    break;

                case "2ⁿᵈ":
                    ToggleSecondFunction();
                    break;

                case "x²":
                    PerformUnaryOperation(x => Math.Pow(x, 2));
                    break;

                case "xʸ":
                    StoreOperator("xʸ");
                    break;

                case "1/x":
                    PerformUnaryOperation(x => 1 / x);
                    break;

                case "|x|":
                    PerformUnaryOperation(Math.Abs);
                    break;


                case "exp":
                    PerformUnaryOperation(Math.Exp);
                    break;

                case "mod":
                    StoreOperator("mod");
                    break;

                case "²√x":
                    PerformUnaryOperation(Math.Sqrt);
                    break;

                case "n!":
                    PerformFactorial();
                    break;

                case "log":
                    PerformUnaryOperation(Math.Log10);
                    break;

                case "ln":
                    PerformUnaryOperation(Math.Log);
                    break;

                case "10ˣ":
                    PerformUnaryOperation(x => Math.Pow(10, x));
                    break;

                case "(":
                case ")":
                    AppendToResult(btnText);
                    break;

                case "÷":
                case "×":
                case "-":
                case "+":
                    StoreOperator(btnText);
                    break;

                case "=":
                    PerformCalculation();
                    break;

                case ".":
                    AppendDecimalPoint();
                    break;

              
                case "sin":
                    PerformUnaryOperation(x => Math.Sin(DegreeToRadian(x)));
                    break;

                case "cos":
                    PerformUnaryOperation(x =>
                    {
                        double result = Math.Cos(DegreeToRadian(x));
                        return Math.Abs(result) < 1e-10 ? 0 : result;
                    });
                    break;

                case "tan":
                    PerformUnaryOperation(x =>
                    {
                        double result = Math.Tan(DegreeToRadian(x));
                        return double.IsInfinity(result) ? double.NaN : result;
                    });
                    break;

                case "cot":
                    PerformUnaryOperation(x =>
                    {
                        double tanValue = Math.Tan(DegreeToRadian(x));
                        return Math.Abs(tanValue) < 1e-10 ? double.NaN : 1 / tanValue;
                    });
                    break;

                case "sec":
                    PerformUnaryOperation(x =>
                    {
                        double cosValue = Math.Cos(DegreeToRadian(x));
                        return Math.Abs(cosValue) < 1e-10 ? double.NaN : 1 / cosValue;
                    });
                    break;

                case "csc":
                    PerformUnaryOperation(x =>
                    {
                        double sinValue = Math.Sin(DegreeToRadian(x));
                        return Math.Abs(sinValue) < 1e-10 ? double.NaN : 1 / sinValue;
                    });
                    break;

                case "hyp":
                    PerformUnaryOperation(Math.Sinh);
                    break;

                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    AppendToResult(btnText);
                    break;

                default:
                    lblScientificResult.Text = "Hata";
                    break;
            }
        }

        private void ResetCalculator()
        {
            lblScientificResult.Text = "0";
            _scientificOperator = null;
            _scientificLastValue = 0;
            _scientificIsNewEntry = true;
        }

        private void Backspace()
        {
            if (lblScientificResult.Text.Length > 1)
                lblScientificResult.Text = lblScientificResult.Text.Substring(0, lblScientificResult.Text.Length - 1);
            else
                lblScientificResult.Text = "0";
        }

        private void PerformUnaryOperation(Func<double, double> operation)
        {
            if (double.TryParse(lblScientificResult.Text, out double value))
            {
                double result = operation(value);
                DisplayFormattedNumber(result);
                _scientificIsNewEntry = true;
            }
        }

        private void PerformFactorial()
        {
            if (int.TryParse(lblScientificResult.Text, out int n) && n >= 0)
            {
                BigInteger result = FactorialBig(n);
                DisplayFormattedNumber(result);
                _scientificIsNewEntry = true;
            }
            else
            {
                lblScientificResult.Text = "Hata";
            }
        }

        private void StoreOperator(string op)
        {
            if (double.TryParse(lblScientificResult.Text, out double value))
            {
                _scientificLastValue = value;
                _scientificOperator = op;
                _scientificIsNewEntry = true;
            }
        }

        private void PerformCalculation()
        {
            if (_scientificOperator == null) return;

            if (double.TryParse(lblScientificResult.Text, out double currentValue))
            {
                double result = 0;

                switch (_scientificOperator)
                {
                    case "xʸ":
                        result = Math.Pow(_scientificLastValue, currentValue);
                        break;
                    case "mod":
                        result = _scientificLastValue % currentValue;
                        break;
                    case "÷":
                        result = currentValue != 0 ? _scientificLastValue / currentValue : double.NaN;
                        break;
                    case "×":
                        result = _scientificLastValue * currentValue;
                        break;
                    case "-":
                        result = _scientificLastValue - currentValue;
                        break;
                    case "+":
                        result = _scientificLastValue + currentValue;
                        break;
                }

                DisplayFormattedNumber(result);
                _scientificOperator = null;
                _scientificIsNewEntry = true;
            }
        }

        private void AppendToResult(string input)
        {
            if (_scientificIsNewEntry || lblScientificResult.Text == "0")
            {
                lblScientificResult.Text = input;
                _scientificIsNewEntry = false;
            }
            else
            {
                lblScientificResult.Text += input;
            }
        }

        private void AppendDecimalPoint()
        {
            if (!_scientificIsNewEntry && !lblScientificResult.Text.Contains("."))
                lblScientificResult.Text += ".";
        }

        private void ToggleSecondFunction()
        {
            
        }

        private BigInteger FactorialBig(int n)
        {
            BigInteger result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;
            return result;
        }

        private void OnTrigToggleClicked(object sender, EventArgs e)
        {
            TrigonometryPanel.IsVisible = !TrigonometryPanel.IsVisible;
        }

        private void DisplayFormattedNumber(object value)
        {
            if (value is BigInteger bigInt)
            {
                lblScientificResult.Text = bigInt.ToString().Length > 10 ? bigInt.ToString("E2") : bigInt.ToString();
            }
            else if (value is double num)
            {
                lblScientificResult.Text = Math.Abs(num) < 1e-10 ? "0" : num.ToString("G10");
            }
        }


        private double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
}
