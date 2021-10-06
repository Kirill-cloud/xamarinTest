using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace calculator
{
    public partial class MainPage : ContentPage
    {
        Stopwatch clock = new Stopwatch();
        List<CancellationTokenSource> tokenSources = new List<CancellationTokenSource>();

        private double width;
        private double height;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    //horizontal
                    Grid.SetRow(Frame2,0);
                    Grid.SetColumn(Frame2,1);
                }
                else
                {
                    Grid.SetRow(Frame2, 1);
                    Grid.SetColumn(Frame2, 0);
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Text.Text += (sender as Button).Text;
        }
        private void arrow(object sender, EventArgs e)
        {
            //if (Text.Text.Length>0 )
            //{
            //    Text.Text = Text.Text.Substring(0, Text.Text.Length - 1);
            //}
        }
        private void Clear(object sender, EventArgs e)
        {
            Text.Text = String.Empty;
        }
        private void Calculate(object sender, EventArgs e)
        {
            Text.Text = Calculate(Text.Text);
        }
        private string Calculate(string text)
        {
            var operands = Text.Text.Split(new char[] { '+', '-', '*', '/' });

            if (operands.Length == 2)
            {
                Dictionary<Char, int> ops = new Dictionary<char, int>() {
                { '+', 0 },
                { '-', 0 },
                { '/', 0 },
                { '*', 0 },
            };

                foreach (var item in Text.Text)
                {
                    if (ops.ContainsKey(item))
                    {
                        ops[item]++;
                    }
                }
                int count = 0;
                foreach (var item in ops)
                {
                    count += item.Value;
                }

                char operation;

                if (count == 1)
                {
                    foreach (var item in ops)
                    {
                        if (item.Value == 1)
                        {
                            operation = item.Key;
                            try
                            {
                                return TrueCalculate(
                                    Convert.ToInt32(operands[0]),
                                    Convert.ToInt32(operands[1]),
                                    operation).ToString();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    return TrueCalculate(
                                        Convert.ToDouble(operands[0]),
                                        Convert.ToDouble(operands[1]),
                                        operation).ToString();
                                }
                                catch (Exception)
                                {
                                    return "Ошибка конвертации";
                                }
                            }

                        }
                    }
                }
            }
            return "Ошибка!";
        }

        private double TrueCalculate(double v1, double v2, char operation)
        {
            switch (operation)
            {
                case '*': return v1 * v2;
                case '/': return v1 / v2;
                case '+': return v1 + v2;
                case '-': return v1 - v2;
                default: return 0;
            }
        }

        private void Button_Released(object sender, EventArgs e)
        {
            clock.Stop();

            foreach (var item in tokenSources)
            {
                item.Cancel();
            }
        }
        private void Button_Pressed(object sender, EventArgs e)
        {
            clock.Restart();

            tokenSources.Add(new CancellationTokenSource());
            var token = tokenSources.Last();
            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    eraseAsync();
                    Task.Delay(1);
                };
            });
        }
        void eraseAsync()
        {
            if (clock.IsRunning && clock.ElapsedMilliseconds % 250 == 0)
            {
                Text.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    if (Text.Text.Length > 0)
                    {
                        Text.Text = Text.Text.Substring(0, Text.Text.Length - 1);
                    }
                });

            }
        }
    }
}
