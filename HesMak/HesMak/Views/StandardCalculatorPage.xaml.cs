using Microsoft.Maui.Controls;
using System;

namespace HesMak.Views
{
    public partial class StandardCalculatorPage : ContentPage
    {
        private double _currentValue = 0;
        private double _lastValue = 0;
        private string _operator = null;
        private bool _isNewEntry = true;

        public StandardCalculatorPage()
        {
            InitializeComponent();
        }

        void OnNumberClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            string digit = btn.Text;

            if (_isNewEntry || lblResult.Text == "0")
            {
                lblResult.Text = digit;
                _isNewEntry = false;
            }
            else
            {
                lblResult.Text += digit;
            }
        }

        void OnOperatorClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            _operator = btn.Text;
            _lastValue = double.Parse(lblResult.Text);
            _isNewEntry = true;
        }

        void OnEqualsClicked(object sender, EventArgs e)
        {
            _currentValue = double.Parse(lblResult.Text);
            double result = 0;

            switch (_operator)
            {
                case "+":
                    result = _lastValue + _currentValue;
                    break;
                case "-":
                    result = _lastValue - _currentValue;
                    break;
                case "×":
                    result = _lastValue * _currentValue;
                    break;
                case "÷":
                    if (_currentValue != 0)
                        result = _lastValue / _currentValue;
                    else
                        result = 0; 
                    break;
                case "%":
                   
                    result = _lastValue * (_currentValue / 100.0);
                    break;
                case "1/x":
                    if (_currentValue != 0)
                        result = 1 / _currentValue;
                    else
                        result = 0;
                    break;
                case "x²":
                    result = _currentValue * _currentValue;
                    break;
                case "²√x":
                  
                    result = Math.Sqrt(_currentValue);
                    break;
            }

            lblResult.Text = result.ToString();
            _isNewEntry = true;
            _operator = null;
        }

        void OnClearClicked(object sender, EventArgs e)
        {
            
            _currentValue = 0;
            _lastValue = 0;
            _operator = null;
            lblResult.Text = "0";
            _isNewEntry = true;
        }

        void OnClearEntryClicked(object sender, EventArgs e)
        {
          
            lblResult.Text = "0";
            _isNewEntry = true;
        }

        void OnBackspaceClicked(object sender, EventArgs e)
        {
            if (lblResult.Text.Length > 1 && !_isNewEntry)
            {
                lblResult.Text = lblResult.Text.Substring(0, lblResult.Text.Length - 1);
            }
            else
            {
                lblResult.Text = "0";
                _isNewEntry = true;
            }
        }

        void OnSignChangeClicked(object sender, EventArgs e)
        {
            double value = double.Parse(lblResult.Text);
            value = -value;
            lblResult.Text = value.ToString();
        }

        void OnDecimalClicked(object sender, EventArgs e)
        {
            if (!lblResult.Text.Contains("."))
            {
                lblResult.Text += ".";
                _isNewEntry = false;
            }
        }
    }
}
